using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRepairService.DAOs;
using CarRepairService.Models;
using CarRepairWebApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace CarRepairWebApi.Controllers
{
    [Route("api/incident")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        private readonly ICarRepairDAO _db;

        /// <summary>
        /// Creates a new Incident controller.
        /// </summary>
        /// <param name="dao">DAO the controller requires.</param>
        public IncidentController(ICarRepairDAO dao)
        {
            _db = dao;
        }

        #region Incident

        /// <summary>
        /// Adds a new incident to the database from input of the front end.
        /// </summary>
        /// <param name="model">Inputs for the new incident</param>
        /// <returns></returns>
        [HttpPost("new")]
        [Authorize(Roles = "Customer")]
        public IActionResult AddIncident([FromBody] VehicleIncidentViewModel model)
        {
            IActionResult result = null;

            try
            {
                if (ModelState.IsValid)
                {
                    Vehicle vehicle = new Vehicle
                    {
                        Color = model.Color,
                        Make = model.Make,
                        Model = model.Model,
                        UserId = _db.GetUserIdByUsername(User.Identity.Name),
                        Vin = model.Vin,
                        Year = model.Year,
                    };

                    Incident incident = new Incident
                    {
                        Description = model.Description
                    };

                    incident.VehicleId = _db.AddVehicleItems(vehicle);

                    _db.AddIncident(incident);

                    result = Ok();
                }
                else
                {
                    result = BadRequest(new { Message = "Required fields are not filled out properly" });
                }
            }
            catch
            {
                result = BadRequest(new { Message = "Adding incident failed." });
            }

            return result;
        }

        /// <summary>
        /// Returns a list of all the incidents for that user.
        /// </summary>
        /// <param name="username">Username to pull the incidents for</param>
        /// <returns></returns>
        [HttpGet("{username}")]
        [Authorize]
        public IActionResult GetIncidentsByUser(string username)
        {
            IActionResult result = Unauthorized();

            List<DisplayIncidentsViewModel> model = new List<DisplayIncidentsViewModel>();

            try
            {
                int userId = _db.GetUserIdByUsername(username);
                List<Incident> userIncidents = _db.GetIncidentsByUser(userId);

                foreach (var incident in userIncidents)
                {
                    DisplayIncidentsViewModel modelLine = new DisplayIncidentsViewModel();
                    modelLine.Incident = incident;
                    modelLine.Vehicle = _db.GetVehicleByID(incident.VehicleId);

                    List<ItemizedIncidentLine> repairLines = _db.GetItemizedLines(incident.Id);

                    modelLine.Status = incident.GetStatus(repairLines);

                    model.Add(modelLine);
                }

                result = Ok(model);
            }
            catch (Exception ex)
            {
                result = BadRequest(new { Message = "Failed to retrieve incidents." });
            }

            return result;
        }

        /// <summary>
        /// Retrieves list of all incidents.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Employee, Administrator")]
        public IActionResult GetIncidents()
        {
            // Check if user is employee or admin
            IActionResult result = Unauthorized();

            List<DisplayIncidentsViewModel> model = new List<DisplayIncidentsViewModel>();

            try
            {
                List<Incident> incidents = _db.GetIncidents();

                foreach (var incident in incidents)
                {
                    DisplayIncidentsViewModel modelLine = new DisplayIncidentsViewModel();
                    modelLine.Incident = incident;
                    modelLine.Vehicle = _db.GetVehicleByID(incident.VehicleId);

                    List<ItemizedIncidentLine> repairLines = _db.GetItemizedLines(incident.Id);

                    modelLine.Status = incident.GetStatus(repairLines);

                    model.Add(modelLine);
                }

                result = Ok(model);
            }
            catch (Exception ex)
            {
                result = BadRequest(new { Message = "Failed to retrieve incidents." });
            }

            return result;
        }

        /// <summary>
        /// Updates database to reflect the customer has paid for the incident.
        /// </summary>
        /// <param name="incidentId"> Incident id that was paid for.</param>
        /// <returns></returns>
        [HttpPut("paid")]
        [Authorize(Roles = "Employee, Administrator")]
        public IActionResult IncidentPaid([FromBody] PayIncidentViewModel model)
        {
            IActionResult result = Unauthorized();

            try
            {
                if (ModelState.IsValid)
                {
                    _db.IncidentPaid(model.IncidentId);
                    _db.AddPickUpDate(model.IncidentId, model.CompletedByDate);
                    result = Ok();
                }
                else
                {
                    result = BadRequest(new { Message = "Required fields are not filled out properly" });
                }
            }
            catch
            {
                result = BadRequest(new { Message = "Failed to mark incident paid." });
            }

            return result;
        }

        /// <summary>
        /// Updates database to reflect the repair has been completed.
        /// </summary>
        /// <param name="incidentId"> Incident id that was completed.</param>
        /// <returns></returns>
        [HttpPut("complete")]
        [Authorize(Roles = "Employee, Administrator")]
        public IActionResult IncidentComplete([FromBody] CompleteIncidentViewModel model)
        {
            IActionResult result = Unauthorized();

            try
            {
                if (ModelState.IsValid)
                {
                    _db.IncidentComplete(model.IncidentId);
                    result = Ok();
                }
                else
                {
                    result = BadRequest(new { Message = "Required fields are not filled out properly" });
                }
            }
            catch
            {
                result = BadRequest(new { Message = "Failed to mark incident complete." });
            }

            return result;
        }

        #endregion

        #region LineItem

        /// <summary>
        /// Pulls the repair lines of the incident to display.
        /// </summary>
        /// <param name="incidentId">Incient to pull repair lines for</param>
        /// <returns></returns>
        [HttpGet("repairlines/{incidentId}")]
        [Authorize]
        public IActionResult GetRepairItems(int incidentId)
        {
            IActionResult result = Unauthorized();

            List<ItemizedIncidentLine> incidentDetailsViewModel = new List<ItemizedIncidentLine>();

            try
            {
                var repairLinesItems = _db.GetItemizedLines(incidentId);
                result = Ok(repairLinesItems);
            }
            catch (Exception ex)
            {
                result = BadRequest(new { Message = "Get repair Lines failed." });
            }

            return result;
        }

        /// <summary>
        /// Adds a new line item to a incident.
        /// </summary>
        /// <param name="model">Details for the line to be added</param>
        /// <returns></returns>
        [HttpPost("line/add")]
        [Authorize(Roles = "Employee, Administrator")]
        public IActionResult AddNewItemizedLine([FromBody] RepairLineViewModel model)
        {
            IActionResult result = Unauthorized();

            try
            {
                if (ModelState.IsValid)
                {
                    ItemizedIncidentLine itemizedIncidentLine = new ItemizedIncidentLine
                    {
                        Cost = model.Cost,
                        Description = model.Description,
                        TimeHours = model.TimeHours,
                        IncidentId = model.IncidentId,
                        Approved = false,
                        Declined = false
                    };

                    //Check if incidents exists
                    Incident existingIncident = _db.GetIncidentById(model.IncidentId);

                    //if vehicle does exist, add incident to vehicle
                    if (existingIncident != null)
                    {
                        itemizedIncidentLine.IncidentId = existingIncident.Id;
                        itemizedIncidentLine.Id = _db.AddItemizedLine(itemizedIncidentLine);
                        result = Ok(itemizedIncidentLine);
                    }
                }
                else
                {
                    result = BadRequest(new { Message = "Incident does not Exist." });
                }
            }
            catch
            {
                result = BadRequest(new { Message = "Adding new repair line failed." });
            }

            return result;
        }

        /// Updates database to approve the item customer marks as accepted.
        /// </summary>
        /// <param name="lineId">line item to approve</param>
        /// <returns></returns>
        [HttpPut("line/approve")]
        [Authorize(Roles = "Customer")]
        public IActionResult ApproveLineItem([FromBody] ApproveRepairViewModel model)
        {
            IActionResult result = Unauthorized();

            try
            {
                ItemizedIncidentLine line = _db.GetItemizedLineById(model.LineId);

                if (!line.Declined)
                {
                    _db.ApproveLineItem(model.LineId);
                    result = Ok();
                }
                else
                {
                    result = BadRequest(new { Message = "Line declined already, can't approve now." });
                }
            }
            catch
            {
                result = BadRequest(new { Message = "Approving repair line failed." });
            }

            return result;
        }

        /// <summary>
        /// Updates database to approve the item customer marks as declined.
        /// </summary>
        /// <param name="model">line item to approve</param>
        /// <returns></returns>
        [HttpPut("line/decline")]
        [Authorize(Roles = "Customer")]
        public IActionResult DeclineLineItem([FromBody] ApproveRepairViewModel model)
        {
            IActionResult result = Unauthorized();

            try
            {
                ItemizedIncidentLine line = _db.GetItemizedLineById(model.LineId);

                if (!line.Approved)
                {
                    _db.DeclineLineItem(model.LineId);
                    result = Ok();
                }
                else
                {
                    result = BadRequest(new { Message = "Line approved already, can't decline now." });
                }
            }
            catch
            {
                result = BadRequest(new { Message = "Declining repair line failed." });
            }

            return result;
        }

        [HttpPut("line/edit")]
        [Authorize(Roles = "Employee, Administrator")]
        public IActionResult EditLineItem(UpdateRepairItemViewModel model)
        {
            IActionResult result = Unauthorized();

            try
            {
                ItemizedIncidentLine line = _db.GetItemizedLineById(model.LineId);

                
                line.Description = model.Description;
                line.Cost = model.Cost;
                line.TimeHours = model.TimeHours;
                

                _db.EditLineItem(line);
            }
            catch
            {
                result = BadRequest(new { Message = "Unable to update Incident Line." });
            }

            return result;
        }

        #endregion
    }
}
