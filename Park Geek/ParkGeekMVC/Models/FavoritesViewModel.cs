using ParkGeek.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkGeekMVC.Models
{
    public class FavoritesViewModel
    {
        public string ParkCode { get; set; }
        public string ParkName { get; set; }
        public int NumVotes { get; set; }
        public string ParkDesc { get; set; }

        public IList<ParkModel> FavParks { get; set; } = new List<ParkModel>();
        public IList<SurveyResultModel> surveys { get; set; } = new List<SurveyResultModel>();
    }
}
