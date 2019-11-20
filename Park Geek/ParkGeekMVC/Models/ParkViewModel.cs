using ParkGeek.DAL.Models;
using ParkGeekMVC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkGeek.MVC.Models
{
    public class ParkViewModel
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
        public int FiveDayForecastValue { get; set; }
        public int LowTemp { get; set; }
        public int HighTemp { get; set; }
        public string ForeCast { get; set; }
        public ParkModel Park { get; set; }

        public IList<WeatherModel> Weather { get; set; }
        public IList<ParkModel> Parks { get; set; } = new List<ParkModel>();

        public int Convert(int temp)
        {
            int finalTemp = 0;
            int first = temp - 32;
            int second = first * 5;
            finalTemp = second / 9;
            return finalTemp;

        }


    }
}
