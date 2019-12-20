using System;
using System.Collections.Generic;
using System.Text;

namespace CarRepairService.Models
{
    [Serializable]
    public class Incident : Item
    {
        private const string COMPLETED = "Completed";
        private const string REPAIRING = "In Progress";
        private const string EVALUATION = "Awaiting Evaluation";
        private const string NEEDPAYMENT = "Awaiting Payment";
        private const string NEEDAPPROVAL = "Awaiting Approval";

        public int VehicleId { get; set; }
        public string Description { get; set; }
        public DateTime SubmittedDate { get; set; } = DateTime.Now;
        public DateTime? PickupDate { get; set; } = null;
        public bool Paid { get; set; } = false;
        public bool Completed { get; set; } = false;

        /// <summary>
        /// Returns the status of the incident based on its state and the state
        /// of it's repair lines.
        /// </summary>
        /// <param name="repairLines">Repairlines to check status off</param>
        /// <returns></returns>
        public string GetStatus(List<ItemizedIncidentLine> repairLines)
        {
            string status = "Submitted";
            int pendingLines = 0;

            // Check if there are any pending repair lines.
            foreach (var repairLine in repairLines)
            {
                if ( !(repairLine.Approved || repairLine.Declined) )
                {
                    pendingLines++;
                }
            }

            if (Completed)
            {
                status = COMPLETED;
            }
            else if (PickupDate != null && Paid)
            {
                status = REPAIRING;
            }
            else if (repairLines.Count == 0)
            {
                status = EVALUATION;
            }
            else if (pendingLines != 0)
            {
                status = NEEDAPPROVAL;
            }
            else if (!Paid)
            {
                status = NEEDPAYMENT;
            }

            return status;
        }
    }
}