using ParkGeek.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkGeekMVC.Models
{
    public class SurveyResultViewModel
    {
        public int SurveyID { get; set; }
        public string ParkCode { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public string ActivityLevel { get; set; }


        public IList<SurveyResultModel> survey { get; set; }
        public IList<ParkModel> Parks { get; set; } = new List<ParkModel>();
    }

        
}



