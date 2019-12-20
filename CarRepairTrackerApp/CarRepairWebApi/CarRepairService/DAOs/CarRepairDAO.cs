using CarRepairService.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CarRepairService.DAOs
{
    public class CarRepairDAO : ICarRepairDAO
    {
        #region Variables

        private const string _getLastIdSQL = "SELECT CAST(SCOPE_IDENTITY() as int);";
        private readonly string _connectionString = "";

        #endregion

        public CarRepairDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region User

        /// <summary>
        /// Adds a user to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Id of the newly generated user</returns>
        public int AddUser(User model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                const string sql = "INSERT [user] (username, first_name, last_name, email, phone_number, hash, salt, role_id) " +
                                   "VALUES(@username, @first_name, @last_name, @email, @phone_number, @hash, @salt, @role_id)";

                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@username", model.Username);
                cmd.Parameters.AddWithValue("@first_name", model.FirstName);
                cmd.Parameters.AddWithValue("@last_name", model.LastName);
                cmd.Parameters.AddWithValue("@email", model.Email);
                cmd.Parameters.AddWithValue("@phone_number", model.PhoneNumber);
                cmd.Parameters.AddWithValue("@hash", model.Hash);
                cmd.Parameters.AddWithValue("@salt", model.Salt);
                cmd.Parameters.AddWithValue("@role_id", model.RoleId);
                model.Id = (int)cmd.ExecuteScalar();
            }

            return model.Id;
        }

        /// <summary>
        /// Finds the UserId of a provided username.
        /// </summary>
        /// <param name="username">Username to find Id by</param>
        /// <returns>User Id</returns>
        public int GetUserIdByUsername(string username)
        {
            int userId = -1;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT id FROM [user] WHERE username = @username;";

                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@username", username);
                userId = (int)cmd.ExecuteScalar();
            }

            return userId;
        }

        /// <summary>
        /// Retrieves user from database by id.
        /// </summary>
        /// <param name="userId">Id to search user by</param>
        /// <returns>User</returns>
        public User GetUserById(int userId)
        {
            User user = null;
            const string sql = "SELECT * From [User] WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", userId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    user = GetUserFromReader(reader);
                }
            }

            if (user == null)
            {
                throw new Exception("User does not exist.");
            }

            return user;
        }

        /// <summary>
        /// Searchs for an existing user in the database by username or email.
        /// </summary>
        /// <param name="searchCredential">search parameter</param>
        /// <returns>found user item</returns>
        public User GetUserByEmailOrUsername(string searchCredential)
        {
            User user = null;
            const string sql = "SELECT * FROM [user] " +
                               "WHERE username = @search OR email = @search;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@search", searchCredential);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    user = GetUserFromReader(reader);
                }
            }

            if (user == null)
            {
                throw new Exception("User does not exist.");
            }

            return user;
        }

        /// <summary>
        /// Saves reader data to a role item.
        /// </summary>
        /// <param name="reader">Reader line with data</param>
        /// <returns>New User item</returns>
        private User GetUserFromReader(SqlDataReader reader)
        {
            User item = new User
            {
                Id = Convert.ToInt32(reader["id"]),
                FirstName = Convert.ToString(reader["first_name"]),
                LastName = Convert.ToString(reader["last_name"]),
                Username = Convert.ToString(reader["username"]),
                Email = Convert.ToString(reader["email"]),
                PhoneNumber = Convert.ToString(reader["phone_number"]),
                Salt = Convert.ToString(reader["salt"]),
                Hash = Convert.ToString(reader["hash"]),
                RoleId = Convert.ToInt32(reader["role_id"])
            };

            return item;
        }

        /// <summary>
        /// Gets list of all users from database
        /// </summary>
        /// <returns>list of users</returns>
        public List<User> GetUserItems()
        {
            List<User> user = new List<User>();
            const string sql = "Select * From [User];";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    user.Add(GetUserFromReader(reader));
                }
            }

            return user;
        }

        /// <summary>
        /// Deletes all users in the user table. *USED FOR TESTING ONLY*
        /// </summary>
        public void DeleteAllUsers()
        {
            const string sql = "Delete FROM [User];";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var reader = cmd.ExecuteNonQuery();

            }
        }

        /// <summary>
        /// Checks the database for the current number of users by role.
        /// </summary>
        /// <returns>Count of current users of that role</returns>
        public int NumberOfUsersByRole(int roleId)
        {
            int result = 0;

            const string sql = "SELECT COUNT(role_id) FROM [user] WHERE role_id = @role_id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@role_id", roleId);

                result = (int)cmd.ExecuteScalar();
            }

            return result;
        }

        #endregion

        #region Role

        /// <summary>
        /// Gets all roles from the database.
        /// </summary>
        /// <returns>List of roles</returns>
        public List<Role> GetRoles()
        {
            List<Role> roles = new List<Role>();
            const string sql = "Select * From Role;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    roles.Add(GetRoleFromReader(reader));
                }
            }

            return roles;
        }


        /// <summary>
        /// Saves reader data to a role item.
        /// </summary>
        /// <param name="reader">Reader line with data</param>
        /// <returns>New Role item</returns>
        private Role GetRoleFromReader(SqlDataReader reader)
        {
            Role item = new Role
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = Convert.ToString(reader["Name"])
            };

            return item;
        }

        #endregion

        #region Incident

        /// Adds an incident to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Id of the newly generated incident</returns>
        public int AddIncident(Incident model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                const string sql = "INSERT [incident] (vehicle_id, description, submitted_date, paid, completed) " +
                                   "VALUES(@vehicle_id, @description, @submitted_date, @paid, @completed)";

                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@vehicle_id", model.VehicleId);
                cmd.Parameters.AddWithValue("@description", model.Description);
                cmd.Parameters.AddWithValue("@submitted_date", model.SubmittedDate);
                cmd.Parameters.AddWithValue("@paid", model.Paid);
                cmd.Parameters.AddWithValue("@completed", model.Completed);
                model.Id = (int)cmd.ExecuteScalar();
            }

            return model.Id;
        }

        /// <summary>
        /// Used to get incident by passing in ID. Built for integration tests. 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Incident GetIncidentById(int incidentId)
        {
            Incident incident = null;
            const string sql = "SELECT * From [incident] WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", incidentId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    incident = GetIncidentFromReader(reader);
                }
            }

            if (incident == null)
            {
                throw new Exception("Incident does not exist.");
            }

            return incident;
        }

        /// <summary>
        /// Retrieves a list of a users incidents, searching by Id.
        /// </summary>
        /// <param name="userId">User Id to find incidents by</param>
        /// <returns>List of incidents for user</returns>
        public List<Incident> GetIncidentsByUser(int userId)
        {
            List<Incident> incidents = new List<Incident>();
            const string sql = "SELECT * FROM [incident] JOIN vehicle on incident.vehicle_id = vehicle.id WHERE vehicle.user_id = @user_id; ";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@user_id", userId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    incidents.Add(GetIncidentFromReader(reader));
                }
            }

            if (incidents == null)
            {
                throw new Exception("Incident does not exist.");
            }

            return incidents;
        }

        /// <summary>
        /// Updates incident to mark it as paid.
        /// </summary>
        /// <param name="incidentId"></param>
        /// <returns>Whether it succeded or not</returns>
        public void IncidentPaid(int incidentId)
        {
            const string sql = "UPDATE incident " +
                               "SET paid = 1 WHERE id = @incident_id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@incident_id", incidentId);

                if (cmd.ExecuteNonQuery() != 1)
                {
                    throw new Exception("Failed to mark paid.");
                }
            }
        }

        /// <summary>
        /// Updates incident to mark it as complete.
        /// </summary>
        /// <param name="incidentId"></param>
        /// <returns>Whether it succeded or not</returns>
        public void IncidentComplete(int incidentId)
        {
            const string sql = "UPDATE incident " +
                               "SET completed = 1 WHERE id = @incident_id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@incident_id", incidentId);

                if (cmd.ExecuteNonQuery() != 1)
                {
                    throw new Exception("Failed to mark complete.");
                }
            }
        }

        /// <summary>
        /// Updates incident to add pick up date for completed repair.
        /// </summary>
        /// <param name="incidentId">Id of incident to update</param>
        /// <param name="pickUpDate">Date of pickup</param>
        /// <returns></returns>
        public void AddPickUpDate(int incidentId, DateTime pickUpDate)
        {
            const string sql = "UPDATE incident " +
                               "SET pickup_date = @date WHERE id = @incident_id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@incident_id", incidentId);
                cmd.Parameters.AddWithValue("@date", pickUpDate);

                if (cmd.ExecuteNonQuery() != 1)
                {
                    throw new Exception("Failed to add pickup date.");
                }
            }
        }

        /// <summary>
        /// Gets all incidents from database.
        /// </summary>
        /// <returns>List of incidents</returns>
        public List<Incident> GetIncidents()
        {
            List<Incident> incident = new List<Incident>();
            const string sql = "Select * From [incident];";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            { 
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    incident.Add(GetIncidentFromReader(reader));
                }
            }

            return incident;
        }

        /// <summary>
        /// Used to delete incdients **FOR INTEGRATION TESTS ONLY**
        /// </summary>
        public void DeleteAllIncidents()
        {
            const string sql = "Delete FROM [incident];";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var reader = cmd.ExecuteNonQuery();

            }
        }

        /// <summary>
        /// Saves reader data to a incident item.
        /// </summary>
        /// <param name="reader">Reader line with data</param>
        /// <returns>New vehicle item</returns>
        private Incident GetIncidentFromReader(SqlDataReader reader)
        {
            Incident incident = new Incident();
            incident.Id = Convert.ToInt32(reader["id"]);
            incident.VehicleId = Convert.ToInt32(reader["vehicle_id"]);
            incident.Description = Convert.ToString(reader["description"]);
            incident.Paid = Convert.ToBoolean(reader["paid"]);
            incident.Completed = Convert.ToBoolean(reader["completed"]);
            incident.SubmittedDate = Convert.ToDateTime(reader["submitted_date"]);
            if (reader["pickup_date"] != DBNull.Value)
            {
                incident.PickupDate = Convert.ToDateTime(reader["pickup_date"]);
            }
            return incident;
        }

        #endregion

        #region ItemizedLine

        /// <summary>
        /// Adds an incident by line item to the database.
        /// </summary>
        /// <param name="model">New itemized line to add</param>
        /// <returns>Id of the newly generated incident by line item</returns>
        public int AddItemizedLine(ItemizedIncidentLine model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                const string sql = "INSERT [incident_itemizated] (incident_id, description, cost, time_hours, approved, declined) " +
                                   "VALUES(@incident_id, @description, @cost, @time_hours, @approved, @declined)";

                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@incident_id", model.IncidentId);
                cmd.Parameters.AddWithValue("@description", model.Description);
                cmd.Parameters.AddWithValue("@cost", model.Cost);
                cmd.Parameters.AddWithValue("@time_hours", model.TimeHours);
                cmd.Parameters.AddWithValue("@approved", model.Approved);
                cmd.Parameters.AddWithValue("@declined", model.Declined);
                model.Id = (int)cmd.ExecuteScalar();
            }

            return model.Id;
        }

        /// <summary>
        /// Gets list of all itemized lines from an incident, searching by incident ID.
        /// </summary>
        /// <returns>list of itemzided incident lines</returns>
        public List<ItemizedIncidentLine> GetItemizedLines(int incidentId) 
        {
            List<ItemizedIncidentLine> itemizedIncidentLine = new List<ItemizedIncidentLine>();
            const string sql = "Select * From [incident_itemizated] WHERE incident_id = @incident_id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@incident_id", incidentId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    itemizedIncidentLine.Add(GetItemizedIncidentLineFromReader(reader));
                }
            }

            return itemizedIncidentLine;
        }

        /// <summary>
        /// Retrieves the line item that 
        /// </summary>
        /// <param name="lineId">id of line to return</param>
        /// <returns></returns>
        public ItemizedIncidentLine GetItemizedLineById(int lineId)
        {
            ItemizedIncidentLine line = null;
            const string sql = "SELECT * FROM incident_itemizated WHERE id = @line_id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@line_id", lineId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    line = GetItemizedIncidentLineFromReader(reader);
                }
            }

            if (line == null)
            {
                throw new Exception("Repair line does not exist.");
            }

            return line;
        }

        /// <summary>
        /// Updates and Item line to aprrove it for repair.
        /// </summary>
        /// <param name="itemLineId"></param>
        /// <returns>Whether it succeded or not</returns>
        public void ApproveLineItem(int itemLineId)
        {
            const string sql = "UPDATE incident_itemizated " +
                               "SET approved = 1 WHERE id = @line_id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@line_id", itemLineId);

                if (cmd.ExecuteNonQuery() != 1)
                {
                    throw new Exception("Failed to approve line.");
                }
            }
        }

        /// <summary>
        /// Updates and Item line to decline it for repair.
        /// </summary>
        /// <param name="itemLineId"></param>
        /// <returns>Whether it succeded or not</returns>
        public void DeclineLineItem(int itemLineId)
        {
            const string sql = "UPDATE incident_itemizated " +
                               "SET declined = 1 WHERE id = @line_id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@line_id", itemLineId);

                if (cmd.ExecuteNonQuery() != 1)
                {
                    throw new Exception("Failed to decline line.");
                }
            }
        }

        /// <summary>
        /// Saves reader data to an itemized incident line item.
        /// </summary>
        /// <param name="reader">Reader line with data</param>
        /// <returns>New ItemizedIncidentLine item</returns>
        private ItemizedIncidentLine GetItemizedIncidentLineFromReader(SqlDataReader reader)
        {
            ItemizedIncidentLine itemizedIncidentLine = new ItemizedIncidentLine();
            itemizedIncidentLine.Id = Convert.ToInt32(reader["id"]);
            itemizedIncidentLine.IncidentId = Convert.ToInt32(reader["incident_id"]);
            itemizedIncidentLine.Description = Convert.ToString(reader["description"]);
            itemizedIncidentLine.Cost = Convert.ToDecimal(reader["cost"]);
            itemizedIncidentLine.TimeHours = Convert.ToInt32(reader["time_hours"]);
            itemizedIncidentLine.Approved = Convert.ToBoolean(reader["approved"]);
            itemizedIncidentLine.Declined = Convert.ToBoolean(reader["declined"]);

            return itemizedIncidentLine;
        }

        /// <summary>
        /// Edits fields in the line item then sets line item to unapproved.
        /// </summary>
        /// <param name="itemLineId"></param>
        /// <returns>Whether it succeded or not</returns>
        public int EditLineItem(ItemizedIncidentLine model)
        {
            //pass in new view model and keep the same line id
            const string sql = "UPDATE incident_itemizated " +
                               "SET description = @description, cost = @cost, time_hours = @time_hours, approved = null, declined = null" + 
                               "WHERE incident_id = model.IncidentId ";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

              
                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                    cmd.Parameters.AddWithValue("@incident_id", model.IncidentId);
                    cmd.Parameters.AddWithValue("@description", model.Description);
                    cmd.Parameters.AddWithValue("@cost", model.Cost);
                    cmd.Parameters.AddWithValue("@time_hours", model.TimeHours);
                    model.Id = (int)cmd.ExecuteScalar();

                return model.Id;
               
            }
        }


        #endregion

        #region Vehicle

        /// Adds an vehicle to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Id of the newly generated vehicle</returns>
        public int AddVehicleItems(Vehicle model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                const string sql = "INSERT [vehicle] (vin, user_id, make, model, year, color) " +
                                   "VALUES(@vin, @user_id, @make, @model, @year, @color)";

                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@vin", model.Vin);
                cmd.Parameters.AddWithValue("@user_id", model.UserId);
                cmd.Parameters.AddWithValue("@make", model.Make);
                cmd.Parameters.AddWithValue("@model", model.Model);
                cmd.Parameters.AddWithValue("@year", model.Year);
                cmd.Parameters.AddWithValue("@color", model.Color);
                model.Id = (int)cmd.ExecuteScalar();
            }

            return model.Id;
        }

        /// <summary>
        /// Gets a vehicle by it's ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Vehicle GetVehicleByID(int id)
        {
            Vehicle vehicle = null;
            const string sql = "SELECT * From [Vehicle] WHERE id = @id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    vehicle = GetVehicleFromReader(reader);
                }
            }

            return vehicle;
        }

        /// <summary>
        /// Gets all vehicles from database.
        /// </summary>
        /// <returns>list of vehicles</returns>
        public List<Vehicle> GetVehicles()
        {
            List<Vehicle> vehicle = new List<Vehicle>();
            const string sql = "Select * From vehicle;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    vehicle.Add(GetVehicleFromReader(reader));
                }
            }

            return vehicle;
        }

        /// <summary>
        /// Saves reader data to a vehicle item.
        /// </summary>
        /// <param name="reader">Reader line with data</param>
        /// <returns>New vehicle item</returns>
        private Vehicle GetVehicleFromReader(SqlDataReader reader)
        {
            Vehicle vehicle = new Vehicle
            {
                Id = Convert.ToInt32(reader["id"]),
                Vin = Convert.ToString(reader["vin"]),
                UserId = Convert.ToInt32(reader["user_id"]),
                Make = Convert.ToString(reader["make"]),
                Model = Convert.ToString(reader["model"]),
                Year = Convert.ToInt32(reader["year"]),
                Color = Convert.ToString(reader["color"]),
            };
            return vehicle;
        }

        /// <summary>
        /// Retrieves vehicle from database by id.
        /// </summary>
        /// <param name="userId">Id to search user by</param>
        /// <returns>User</returns>
        public Vehicle GetVehicleByVin(string vehicleVin)
        {
            Vehicle vehicle = null;
            const string sql = "SELECT * From [Vehicle] WHERE Vin = @Vin;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Vin", vehicleVin);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    vehicle = GetVehicleFromReader(reader);
                }
            }

            return vehicle;
        }

        /// <summary>
        /// Deletes all vehicles in the vehicle table. *USED FOR TESTING ONLY*
        /// </summary>
        public void DeleteAllVehicles()
        {
            const string sql = "Delete FROM [Vehicle];";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var reader = cmd.ExecuteNonQuery();

            }
        }

        #endregion
    }
}
