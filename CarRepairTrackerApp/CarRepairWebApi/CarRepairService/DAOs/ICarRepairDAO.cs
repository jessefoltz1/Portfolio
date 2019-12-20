using CarRepairService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRepairService.DAOs
{
    public interface ICarRepairDAO
    {
        #region User

        int AddUser(User model);
        User GetUserById(int userId);
        User GetUserByEmailOrUsername(string searchCredential);
        int GetUserIdByUsername(string username);
        int NumberOfUsersByRole(int roleId);

        #endregion

        #region Role
        List<Role> GetRoles();
        #endregion

        #region Incident

        /// <summary>
        /// Saves a new Incident to the system.
        /// </summary>
        /// <param name="newIncident"></param>
        /// <returns></returns>
        int AddIncident(Incident newIncident);
        List<Incident> GetIncidentsByUser(int userId);
        void IncidentPaid(int incidentId);
        void IncidentComplete(int incidentId);
        void AddPickUpDate(int incidentId, DateTime pickUpDate);
        List<Incident> GetIncidents();
        Incident GetIncidentById(int incidentId);

        #endregion

        #region ItemizedLine

        List<ItemizedIncidentLine> GetItemizedLines(int incidentId);
        ItemizedIncidentLine GetItemizedLineById(int lineId);
        int AddItemizedLine(ItemizedIncidentLine model);
        void ApproveLineItem(int itemLineId);
        void DeclineLineItem(int itemLineId);
        int EditLineItem(ItemizedIncidentLine model);

        #endregion

        #region Vehicle

        /// <summary>
        /// Saves a new Incident to the system.
        /// </summary>
        /// <param name="newVehicle"></param>
        /// <returns></returns>
        int AddVehicleItems(Vehicle newVehicle);
        Vehicle GetVehicleByVin(string vehicleVin);
        Vehicle GetVehicleByID(int id);

        #endregion


    }

}

