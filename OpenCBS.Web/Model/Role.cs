using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCBS.Web.Model
{
    public class Role : EntityBase
    {
        private static readonly IDictionary<string, HashSet<string>> Map = new Dictionary<string, HashSet<string>>();
        private static readonly IList<string> AlwaysAccessible = new List<string>
        {
            "IUserService.ValidateChangePassword",
            "IUserService.ChangeMyPassword"
        };

        static Role()
        {
            // There are two levels of permissions:
            // a) Screen permissions -- define what is accessible to the user 
            // from the UI perspective (menu items, forms, buttons, etc.)
            // b) Service permissions -- define what is accessible at the Service layer.
            //
            // In general, one screen function requires several service permissions. For example,
            // to be able to add a user you have to list all the roles in the system.
            // So, along with IUserService.Add you need IRoleService.FindAll
            //
            // The Map below defines all such entries: screen permission -> list of service permissions
            Map.Add("Security.ViewRole", new HashSet<string>
            {
                "IRoleService.FindAll"
            });
            Map.Add("Security.AddRole", new HashSet<string>
            {
                "IRoleService.FindAll",
                "IRoleService.Validate",
                "IRoleService.Add"
            });
            Map.Add("Security.EditRole", new HashSet<string>
            {
                "IRoleService.FindAll",
                "IRoleService.Validate",
                "IRoleService.FindById",
                "IRoleService.Update"
            });
            Map.Add("Security.DeleteRole", new HashSet<string>
            {
                "IRoleService.FindAll",
                "IRoleService.Delete"
            });

            Map.Add("Security.ViewUser", new HashSet<string>
            {
                "IRoleService.FindAll",
                "IUserService.FindAll"
            });
            Map.Add("Security.AddUser", new HashSet<string>
            {
                "IRoleService.FindAll",
                "IUserService.FindAll",
                "IUserService.Validate",
                "IUserService.Add"
            });
            Map.Add("Security.EditUser", new HashSet<string>
            {
                "IRoleService.FindAll",
                "IUserService.FindById",
                "IUserService.FindAll",
                "IUserService.Validate",
                "IUserService.Update"
            });
            Map.Add("Security.ChangeUserPassword", new HashSet<string>
            {
                "IUserService.FindAll",
                "IUserService.ChangePassword"
            });
            Map.Add("Security.DeleteUser", new HashSet<string>
            {
                "IUserService.FindAll",
                "IUserService.Delete"
            });
            Map.Add("EntryFee.View", new HashSet<string>
            {
                "IEntryFeeService.FindAll"
            });
            Map.Add("EntryFee.Add" , new HashSet<string>
            {
                "IEntryFeeService.FindAll",
                "IEntryFeeService.Validate",
                "IEntryFeeService.Add"
            });
            Map.Add("EntryFee.Edit", new HashSet<string>
            {
                "IEntryFeeService.FindAll",
                "IEntryFeeService.Validate",
                "IEntryFeeService.FindById",
                "IEntryFeeService.Update"
            });
            Map.Add("EntryFee.Delete", new HashSet<string>
            {
                "IEntryFeeService.FindAll",
                "IEntryFeeService.Delete"
            });
            Map.Add("LoanProduct.View", new HashSet<string>
            {
                "ILoanProductService.FindAll"
            });
            Map.Add("LoanProduct.Add", new HashSet<string>
            {
                "IEntryFeeService.FindAll",
                "IEntryFeeService.FindById",
                "ILoanProductService.GetReferenceData",
                "ILoanProductService.FindAll",
                "ILoanProductService.Validate",
                "ILoanProductService.Add"
            });
            Map.Add("LoanProduct.Edit", new HashSet<string>
            {
                "IEntryFeeService.FindAll",
                "IEntryFeeService.FindById",
                "ILoanProductService.GetReferenceData",
                "ILoanProductService.FindAll",
                "ILoanProductService.Validate",
                "ILoanProductService.FindById",
                "ILoanProductService.Update"
            });
            Map.Add("LoanProduct.Delete", new HashSet<string>
            {
                "ILoanProductService.FindAll",
                "ILoanProductService.Delete"
            });
            Map.Add("Configuration.ViewExoticSchedule", new HashSet<string>
            {
                "IExoticScheduleService.FindAll"
            });
        }

        public string Name { get; set; }
        public IList<string> Permissions { get; set; }

        public bool Can(string permission)
        {
            if (Permissions == null)
                throw new ArgumentException(string.Format("No permissions for role {0}", Name));
            return Permissions.Contains(permission);
        }

        public bool HasServicePermission(string servicePermission)
        {
            if (Permissions == null)
                throw new ArgumentException(string.Format("No permissions for role {0}", Name));
            var list = Permissions.Where(p => Map.ContainsKey(p)).Select(p => Map[p]);
            var set = new HashSet<string>();
            foreach (var item in list) set.UnionWith(item);
            return set.Contains(servicePermission) || AlwaysAccessible.Contains(servicePermission);
        }
    }
}
