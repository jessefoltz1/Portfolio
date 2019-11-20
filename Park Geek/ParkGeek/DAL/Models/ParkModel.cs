using System;
using System.Collections.Generic;
using System.Text;

namespace ParkGeek.DAL.Models
{
    public class ParkModel
    {
        public string ParkCode { get; set; }
        public string ParkName { get; set; }
        public string State { get; set; }
        public int Acerage { get; set; }
        public int ElevationFeet { get; set; }
        public int MilesOfTrails { get; set; }
        public int NumOfCampsites { get; set; }
        public string Climate { get; set; }
        public int YearFounded { get; set; }
        public string Quote { get; set; }
        public string QuoteSource { get; set; }
        public int AnnualVisitorCount { get; set; }
        public string ParkDescription { get; set; }
        public int EntryFee { get; set; }
        public int NumOfAnimalSpecies { get; set; }




    }
}
