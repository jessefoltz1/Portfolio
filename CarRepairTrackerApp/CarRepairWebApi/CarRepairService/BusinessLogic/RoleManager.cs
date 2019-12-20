using CarRepairService.DAOs;
using CarRepairService.Models;
using System.Collections.Generic;

namespace CarRepairService.BusinessLogic
{
    /// <summary>
    /// Holds a user and manages their permissions
    /// </summary>
    public class RoleManager
    {
        public const string ADMINISTRATOR = "Administrator";
        public const string EMPLOYEE = "Employee";
        public const string CUSTOMER = "Customer";
        public const string UNKOWN = "Unkown";


        private Dictionary<int, Role> _roles = new Dictionary<int, Role>();

        /// <summary>
        /// The user to manage permissions for
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// The name of the user's role
        /// </summary>
        public string RoleName
        {
            get
            {
                return User != null ? _roles[User.RoleId].Name : UNKOWN;
            }
        }

        public int AdministratorRoleId
        {
            get
            {
                return GetIdForRole(ADMINISTRATOR);
            }
        }

        public int EmployeeRoleId
        {
            get
            {
                return GetIdForRole(EMPLOYEE);
            }
        }

        public int CustomerRoleId
        {
            get
            {
                return GetIdForRole(CUSTOMER);
            }
        }

        /// <summary>
        /// Constructor for the role manager. Create this everytime a user changes.
        /// </summary>
        /// <param name="user">The user to get the permissions for</param>
        public RoleManager(ICarRepairDAO db)
        {
            if(_roles.Count == 0)
            {
                var roles = db.GetRoles();
                foreach(var role in roles)
                {
                    _roles.Add(role.Id, role);
                }
            }
        }

        public int GetIdForRole(string roleName)
        {
            int result = Role.InvalidId;

            foreach(var role in _roles)
            {
                if(role.Value.Name == roleName)
                {
                    result = role.Key;
                }
            }

            return result;
        }

        /// <summary>
        /// Specifies if the user has administrator permissions
        /// </summary>
        public bool IsAdministrator
        {
            get
            {
                return RoleName == ADMINISTRATOR;
            }
        }

        /// <summary>
        /// Specifies if the user has employee permissions
        /// </summary>
        public bool IsEmployee
        {
            get
            {
                return RoleName == EMPLOYEE;
            }
        }

        /// <summary>
        /// Specifies if the user has customer permissions
        /// </summary>
        public bool IsCustomer
        {
            get
            {
                return RoleName == CUSTOMER;
            }
        }

        /// <summary>
        /// Specifies if the user has unknown permissions
        /// </summary>
        public bool IsUnknown
        {
            get
            {
                return RoleName == UNKOWN;
            }
        }
    }
}
