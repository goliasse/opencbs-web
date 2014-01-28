using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using Dapper;

namespace OpenCBS.Web.Repository
{
    public static class DapperExtensions
    {
        public static void Update<T>(this IDbConnection connection, T data, IList<string> exclude = null)
        {
            var tableName = GetTableName(typeof (T));

            var paramNames = GetParamNames(data);
            if (exclude != null)
                foreach (var ex in exclude)
                    paramNames.Remove(ex);

            var builder = new StringBuilder();
            builder.Append("update [").Append(tableName).Append("] set ");
            builder.AppendLine(string.Join(",", paramNames.Where(n => n != "Id").Select(p => p + "= @" + p)));
            builder.Append("where Id = @Id");

            var parameters = new DynamicParameters(data);
            connection.Execute(builder.ToString(), parameters);
        }

        public static int Insert<T>(this IDbConnection connection, T data)
        {
            var tableName = GetTableName(typeof(T));

            var o = (object) data;
            var paramNames = GetParamNames(o);
            paramNames.Remove("Id");

            var cols = string.Join(",", paramNames);
            var colsParams = string.Join(",", paramNames.Select(p => "@" + p));
            var builder = new StringBuilder();
            builder.Append("set nocount on insert [");
            builder.Append(tableName);
            builder.Append("] (").Append(cols).Append(")");
            builder.Append("values(").Append(colsParams).Append(") select cast(scope_identity() as int)");
            return connection.Query<int>(builder.ToString(), o).Single();
        }

        public static void Delete<T>(this IDbConnection connection, int id)
        {
            var tableName = GetTableName(typeof(T));
            var sql = "update " + tableName + " set Deleted = 1 where Id = @Id";
            connection.Execute(sql, new { Id = id });
        }

        private static string GetTableName(Type t)
        {
            var tableName = t.Name;
            if (tableName.EndsWith("Row"))
                tableName = tableName.Substring(0, tableName.Length - 3);
            return tableName;
        }

        private static IList<string> GetParamNames(object o)
        {
            if (o is DynamicParameters)
            {
                return (o as DynamicParameters).ParameterNames.ToList();
            }

            var paramNames = new List<string>();
            foreach (var prop in o.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public))
            {
                paramNames.Add(prop.Name);
            }
            return paramNames;
        }
    }
}
