using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using OpenCBS.Web.Interface.Repository;
using OpenCBS.Web.Model;

namespace OpenCBS.Web.Repository
{
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(IConnectionStringProvider connectionStringProvider)
            : base(connectionStringProvider)
        {
        }

        public bool UserExists(int id, string password)
        {
            const string sql = @"select cast(count(*) as bit) from Users where id = @Id and user_pass = @Password";
            using (var connection = GetConnection())
            {
                return connection.Query<bool>(sql, new { Id = id, Password = password }).Single();
            }
        }

        public void ChangePassword(int id, string password)
        {
            const string sql = @"update Users set user_pass = @Password where id = @Id";
            using (var connection = GetConnection())
            {
                connection.Execute(sql, new { Id = id, Password = password });
            }
        }

        public User FindByUsernameAndPassword(string username, string password)
        {
            const string sql = @"
                select 
                    id Id, first_name FirstName, last_name LastName, mail Email, user_name Username, 
                    deleted Deleted, IsSuperuser
                from Users where user_name = @Username and user_pass = @Password

                select r.id Id, r.code Name from Roles r
                inner join UserRole ur on ur.role_id = r.id
                inner join Users u on u.id = ur.user_id
                where u.user_name = @Username and u.user_pass = @Password

                --select rp.RoleId, rp.Permission from RolePermission rp
                --inner join UserRole ur on ur.role_id = rp.RoleId
                --inner join Users u on u.id = ur.user_id
                --where u.user_name = @Username and u.user_pass = @Password
            ";
            using (var connection = GetConnection())
            using (var multi = connection.QueryMultiple(sql, new { Username = username, Password = password }))
            {
                var user = multi.Read<User>().SingleOrDefault();
                if (user == null) return null;
                user.Roles = multi.Read<Role>().ToList().AsReadOnly();
//                var permissions = multi.Read<int, string, Tuple<int, string>>(Tuple.Create, "*").ToList();
//                foreach (var role in user.Roles)
//                {
//                    role.Permissions = permissions.Where(p => p.Item1 == role.Id).Select(p => p.Item2).ToList().AsReadOnly();
//                }
                return user;
            }
        }

        public IList<User> FindAll()
        {
            const string sql = @"
                select id Id, first_name FirstName, last_name LastName, mail Email, user_name Username, 
                    deleted Deleted, IsSuperuser
                from Users

                select user_id, role_id from UserRole
                select id Id, code Name from Roles
                select RoleId, Permission from RolePermission
            ";
            using (var connection = GetConnection())
            using (var multi = connection.QueryMultiple(sql))
            {
                var users = multi.Read<User>().ToList().AsReadOnly();
                var map = multi.Read<int, int, Tuple<int, int>>(Tuple.Create, "*").ToList();
                var roles = multi.Read<Role>().ToList().AsReadOnly();
                var permissionMap = multi.Read<int, string, Tuple<int, string>>(Tuple.Create, "*").ToList();
                foreach (var user in users)
                {
                    var ids = map.Where(m => m.Item1 == user.Id).Select(m => m.Item2).ToList();
                    user.Roles = roles.Where(r => ids.Contains(r.Id)).ToList().AsReadOnly();
                    foreach (var role in user.Roles)
                    {
                        role.Permissions = permissionMap.Where(x => x.Item1 == role.Id).Select(x => x.Item2).ToList().AsReadOnly();
                    }
                }
                return users;
            }
        }

        public User FindById(int id)
        {
            const string sql = @"
                select id Id, first_name FirstName, last_name LastName, mail Email, user_name Username, 
                    deleted Deleted, IsSuperuser
                from Users
                where id = @Id

                select r.id Id, r.code Name from Roles r
                inner join UserRole ur on ur.role_id = r.id
                inner join Users u on u.id = ur.user_id
                where u.id = @Id

                --select rp.RoleId, rp.Permission from RolePermission rp
                --inner join UserRole ur on ur.role_id = rp.RoleId
                --inner join Users u on u.id = ur.user_id
                --where u.id = @Id
            ";
            using (var connection = GetConnection())
            using (var multi = connection.QueryMultiple(sql, new { Id = id }))
            {
                var user = multi.Read<User>().SingleOrDefault();
                if (user == null) return null;
                user.Roles = multi.Read<Role>().ToList().AsReadOnly();
//                var permissions = multi.Read<int, string, Tuple<int, string>>(Tuple.Create, "*").ToList();
//                foreach (var role in user.Roles)
//                {
//                    role.Permissions = permissions.Where(p => p.Item1 == role.Id).Select(p => p.Item2).ToList().AsReadOnly();
//                }
                return user;
            }
        }

        public void Update(User entity)
        {
            using (var connection = GetConnection())
            {
                var sql = @"
                    update Users
                    set first_name = @FirstName, last_name = @LastName, user_name = @Username, mail = @Email
                    where id = @Id
                ";
                connection.Execute(sql, entity);

                sql = @"delete UserRole where user_id = @Id";
                connection.Execute(sql, new { entity.Id });
                var map = entity.Roles.Select(r => new { UserId = entity.Id, RoleId = r.Id });
                sql = @"insert UserRole (user_id, role_id) values (@UserId, @RoleId)";
                connection.Execute(sql, map);
            }
        }

        public int Add(User entity)
        {
            using (var connection = GetConnection())
            {
                var sql = @"
                    insert Users
                    (first_name, last_name, user_name, mail, user_pass, role_code)
                    values (@FirstName, @LastName, @Username, @Email, @Password, 'VISIT')
                    select cast(scope_identity() as int)
                ";
                var id = connection.Query<int>(sql, entity).Single();

                sql = @"insert UserRole (user_id, role_id) values (@UserId, @RoleId)";
                var map = entity.Roles.Select(r => new { UserId = id, RoleId = r.Id });
                connection.Execute(sql, map);
                return id;
            }
        }

        public void Remove(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Execute(@"update Users set deleted = 1 where id = @Id", new { Id = id });
            }
        }
    }
}
