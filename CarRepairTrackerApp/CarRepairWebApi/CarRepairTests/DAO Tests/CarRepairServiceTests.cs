using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using CarRepairService.Models;
using CarRepairService.DAOs;
using System.Data.SqlClient;

namespace CarRepairTests.DAO_Tests
{
    [TestClass]
    public class CarRepairServiceTests
    {
        

        /// <summary>
        /// The transaction for each test.
        /// </summary>
        private TransactionScope transaction;

        

        protected string ConnectionString { get; } = "Data Source=.\\SQLEXPRESS;Initial Catalog=CarRepairTracker;Integrated Security=True";

        

        #region Test Initialize and Cleanup

        [TestInitialize]
        public void Setup()
        {
            // Begin the transaction
            transaction = new TransactionScope();

          

        }

        [TestCleanup]
        public void Cleanup()
        {
            // Roll back the transaction
            transaction.Dispose();
        }

        #endregion

        #region Methods for Tests
        private User CreateFirstTestUser()
        {
            CarRepairDAO dao = new CarRepairDAO(ConnectionString);
            User user = new User();
            user.RoleId = 3;
            user.Username = "K.Schott";
            user.FirstName = "Kevin";
            user.LastName = "Schott";
            user.Email = "myemail@gmail.com";
            user.PhoneNumber = "1-513-555-6666";
            user.Hash = "hash";
            user.Salt = "salt";
            

            return user;
        }

        private User CreateSecondTestUser()
        {
            CarRepairDAO dao = new CarRepairDAO(ConnectionString);
            User user2 = new User();
            user2.RoleId = 3;
            user2.Username = "K.Schott2";
            user2.FirstName = "Kevin";
            user2.LastName = "Schott";
            user2.Email = "mysecondemail@gmail.com";
            user2.PhoneNumber = "1-513-555-6666";
            user2.Hash = "hash";
            user2.Salt = "salt";
           

            return user2;

        }

        private Incident CreateFirstTestIncident(int vehicleId)
        {
            Incident incident = new Incident();
            incident.VehicleId = vehicleId;
            incident.Description = "Car is Broken";
            incident.SubmittedDate = new DateTime(2012, 11, 15);
            incident.Paid = true;

            return incident;
        }

        private Incident CreateSecondTestIncident(int vehicleId)
        {
            Incident incident = new Incident();
            incident.VehicleId = vehicleId;
            incident.Description = "Car is Broken";
            incident.SubmittedDate = new DateTime(2012, 11, 15);
            incident.Paid = true;

            return incident;
        }

        private Vehicle CreateTestVehicle(int newUserID)
        {
            CarRepairDAO dao = new CarRepairDAO(ConnectionString);
            Vehicle vehicle = new Vehicle();
            vehicle.Vin = "VinTest";
            vehicle.UserId = newUserID;
            vehicle.Make = "MakeTest";
            vehicle.Model = "Model Test";
            vehicle.Year = 1990;
            vehicle.Color = "ColorTest";
            

            return vehicle;
        }




        #endregion


        #region User Tests
        [TestMethod]
        public void AddUserTest()
        {
            //// Arrange

            CarRepairDAO dao = new CarRepairDAO(ConnectionString);
            User user = CreateFirstTestUser();
            int newId = dao.AddUser(user);
           
            var newUser = dao.GetUserById(newId);

            Assert.AreEqual(newId, newUser.Id, "ID does not match, check GetUserItemByIdMethod");
            Assert.AreEqual(user.RoleId, newUser.RoleId, "Role ID does not match");
            Assert.AreEqual(user.Username, newUser.Username, "UserName does not match");
            Assert.AreEqual(user.FirstName, newUser.FirstName, "FirstName does not match");
            Assert.AreEqual(user.LastName, newUser.LastName, "LastName does not match");
            Assert.AreEqual(user.Email, newUser.Email, "Email does not match");
            Assert.AreEqual(user.PhoneNumber, newUser.PhoneNumber, "PhoneNumber does not match");
            Assert.AreEqual(user.Hash, newUser.Hash, "Hash does not match");
            Assert.AreEqual(user.Salt, newUser.Salt, "Salt does not match");

        }

        [TestMethod]
        public void GetUserItemsTest()
        {
            
            CarRepairDAO dao = new CarRepairDAO(ConnectionString);

            //delete entire table
            dao.DeleteAllVehicles();
            dao.DeleteAllUsers();


            //create new users
            User user = CreateFirstTestUser();
            int newUserID = dao.AddUser(user);


            User user2 = CreateSecondTestUser();
            int newUser2ID = dao.AddUser(user2);

            List<User> userList = new List<User>();
            userList = dao.GetUserItems();

            Assert.AreEqual(userList.Count, 2, "2 users were not returned");
            Assert.AreEqual(userList[0].Id, newUserID, "First user's ID does not match first user in list");
            Assert.AreEqual(userList[1].Id, newUser2ID, "Second user's ID does not match second user in list");

        }

        #endregion

        #region Role Tests
        [TestMethod]
        public void GetRolesTest()
        {
            CarRepairDAO dao = new CarRepairDAO(ConnectionString);
            List<Role> RolesList = new List<Role>();
            RolesList = dao.GetRoles();

            Assert.AreEqual(RolesList[0].Id, 1, "Administrator ID does not equal 1");
            Assert.AreEqual(RolesList[0].Name, "Administrator", "ID 1 is not named Administrator");
            Assert.AreEqual(RolesList[1].Id, 2, "Employee ID does not equal 2");
            Assert.AreEqual(RolesList[1].Name, "Employee", "ID 2 is not named Employee");
            Assert.AreEqual(RolesList[2].Id, 3, "Customer ID does not equal 3");
            Assert.AreEqual(RolesList[2].Name, "Customer", "ID 1 is not named Customer");

        }

        #endregion

        #region Incidents Tests
        [TestMethod]
        public void AddIncidentTest()
        {
            CarRepairDAO dao = new CarRepairDAO(ConnectionString);

            //add user
            User user = CreateFirstTestUser();
            int userId = dao.AddUser(user);

            //add vehicle and set incident to that id

            Vehicle vehicle = CreateTestVehicle(userId);
            int vehicleId = dao.AddVehicleItems(vehicle);


            Incident incident = CreateFirstTestIncident(vehicleId);
            
            int newId = dao.AddIncident(incident);
            var newUser = dao.GetIncidentById(newId);

            Assert.AreEqual(newId, newUser.Id, "ID does not match, check GetIncidentById Method");
            Assert.AreEqual(incident.VehicleId, newUser.VehicleId, "Vehicle ID does not match");
            Assert.AreEqual(incident.Description, newUser.Description, "Description does not match");
            Assert.AreEqual(incident.SubmittedDate, newUser.SubmittedDate, "SubmittedDate does not match");
            Assert.AreEqual(incident.Paid, newUser.Paid, "Paid bool does not match");

        }

        [TestMethod]
        public void GetIncidentsTest()
        {
            CarRepairDAO dao = new CarRepairDAO(ConnectionString);
            dao.DeleteAllIncidents();

            User user = CreateFirstTestUser();
            int userId = dao.AddUser(user);

            Vehicle vehicle = CreateTestVehicle(userId);
            int vehicleId = dao.AddVehicleItems(vehicle);


            Incident incident1 = CreateFirstTestIncident(vehicleId);
            int newIncidentID = dao.AddIncident(incident1);

            Incident incident2 = CreateSecondTestIncident(vehicleId);
            int newIncidentID2 = dao.AddIncident(incident2);

            List<Incident> incidentList = new List<Incident>();
            incidentList = dao.GetIncidents();

            Assert.AreEqual(incidentList.Count, 2, "2 users were not returned");
            Assert.AreEqual(incidentList[0].Id, newIncidentID, "First user's ID does not match first user in list");
            Assert.AreEqual(incidentList[1].Id, newIncidentID2, "Second user's ID does not match second user in list");

        }

        [TestMethod]
        public void GetIncidentsByUserTest()
        {
            CarRepairDAO dao = new CarRepairDAO(ConnectionString);
            List<Incident> incidentsList = new List<Incident>();

            //delete all vehicles
            dao.DeleteAllVehicles();

            //delete users
            dao.DeleteAllUsers();

            //delete incidents  
            dao.DeleteAllIncidents();

            //delete all vehicles
            dao.DeleteAllVehicles();

            //add user and return id
            User user = CreateFirstTestUser();
            int newUserID = dao.AddUser(user);

            //add vehicle
            Vehicle vehicle = CreateTestVehicle(newUserID);
            int vehicleId = dao.AddVehicleItems(vehicle);
            


            //add incidents with new user id
            Incident incident1 = CreateFirstTestIncident(vehicleId);
            int newIncidentID = dao.AddIncident(incident1);

            Incident incident2 = CreateSecondTestIncident(vehicleId);
            int newIncidentID2 = dao.AddIncident(incident2);

            //call get method and add to incidents list

            incidentsList = dao.GetIncidentsByUser(newUserID);

            Assert.AreEqual(incidentsList.Count, 2, "2 incidents were not returned");
            Assert.AreEqual(incidentsList[0].VehicleId, vehicleId, "First incidents vehicle ID does not match");
            Assert.AreEqual(incidentsList[1].VehicleId, vehicleId, "Second incidents vehicle ID does not match");


        }

        #endregion
    }
}
