using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRepairService.BusinessLogic;
using CarRepairService.DAOs;
using CarRepairService.Models;
using CarRepairWebApi.Models;
using CarRepairWebApi.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRepairWebApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly ICarRepairDAO _db;

        /// <summary>
        /// Creates a new account controller.
        /// </summary>
        /// <param name="tokenGenerator">A token generator used when creating auth tokens.</param>
        /// <param name="db">Access to the BuddyBux database.</param>
        public AccountController(ITokenGenerator tokenGenerator, ICarRepairDAO db)
        {
            _tokenGenerator = tokenGenerator;
            _db = db;
        }

        #region Basic

        /// <summary>
        /// Registers a user and provides a bearer token.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public IActionResult Register(RegisterViewModel model)
        {
            IActionResult result = null;

            // Does user already exist
            try
            {
                if (ModelState.IsValid)
                {
                    // Generate a password hash
                    var pwdMgr = new PasswordManager(model.Password);
                    var roleMgr = new RoleManager(_db);

                    // Create a user object
                    var user = new User
                    {
                        Hash = pwdMgr.Hash,
                        Salt = pwdMgr.Salt,
                        RoleId = roleMgr.CustomerRoleId,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Username = model.Username,
                        PhoneNumber = model.PhoneNumber,
                    };

                    // Save the user account
                    _db.AddUser(user);

                    roleMgr.User = _db.GetUserByEmailOrUsername(model.Username);

                    // Generate a token
                    var token = _tokenGenerator.GenerateToken(roleMgr.User.Username, roleMgr.RoleName);

                    result = Ok(token);
                }
                else
                {
                    result = BadRequest(new { Message = "Required fields not filled out correctly" });
                }
            }
            catch (Exception)
            {
                result = BadRequest(new { Message = "Username or Email has already been taken." });
            }

            // Return the token
            return result;
        }

        /// <summary>
        /// Authenticates the user and provides a bearer token.
        /// </summary>
        /// <param name="model">An object including the user's credentials.</param> 
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            // Assume the user is not authorized
            IActionResult result = Unauthorized();

            try
            {
                if (ModelState.IsValid)
                {
                    var roleMgr = new RoleManager(_db);
                    roleMgr.User = _db.GetUserByEmailOrUsername(model.LoginCredentials);

                    // Generate a password hash
                    var pwdMgr = new PasswordManager(model.Password, roleMgr.User.Salt);

                    if (pwdMgr.Verify(roleMgr.User.Hash))
                    {
                        // Create an authentication token
                        var token = _tokenGenerator.GenerateToken(roleMgr.User.Username, roleMgr.RoleName);

                        // Switch to 200 OK
                        result = Ok(token);
                    }
                }
                else
                {
                    result = BadRequest(new { Message = "Required fields not filled out correctly" });
                }
            }
            catch (Exception)
            {
                result = BadRequest(new { Message = "Username or password is invalid." });
            }

            return result;
        }

        #endregion

        #region Admin

        /// <summary>
        /// Add a new User or Admin if poster is Admin.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register/employee")]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddEmployeeOrAdim([FromBody] EmployeeAdminRegisterViewModel model)
        {
            IActionResult result = null;

            try
            { 
           //pick role number by name (dropdown) and return the value
           if(ModelState.IsValid)
            {
                // Generate a password hash
                var pwdMgr = new PasswordManager(model.Password);
                var roleMgr = new RoleManager(_db);

                // Create a user object
                var user = new User
                {
                    Hash = pwdMgr.Hash,
                    Salt = pwdMgr.Salt,
                    RoleId = roleMgr.GetIdForRole(model.RoleName),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Username = model.Username,
                    PhoneNumber = model.PhoneNumber,
                };

                // Save the user account
                _db.AddUser(user);

                // Generate a token
                var token = _tokenGenerator.GenerateToken(user.Username, roleMgr.RoleName);

                result = Ok(token);
            }
            else
            {
                result = BadRequest(new { Message = "Required fields not filled out correctly" });
            }
            }
            catch (Exception)
            {
                result = BadRequest(new { Message = "Username or Email has already been taken." });
            }

            // Return the token
            return result;
        }

        /// <summary>
        /// Retrieves customer by id to get their information for displaying on incident.
        /// </summary>
        /// <param name="customerId">id to find customer by</param>
        /// <returns>basic customer information</returns>
        [HttpGet("user/{customerId}")]
        [Authorize]
        public IActionResult GetCustomerbyUserID(int customerId)
        {
            IActionResult result = null;

            try
            {
                User model = _db.GetUserById(customerId);

                // Only let the information we want leave, void the rest
                model.Hash = null;
                model.Salt = null;
                model.RoleId = -1;

                result = Ok(model);
            }
            catch (Exception ex)
            {
                result = BadRequest(new { Message = "Failed to retrieve customer." });
            }

            return result;
        }

        #endregion
    }
}