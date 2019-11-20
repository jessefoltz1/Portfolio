using ParkGeekMVC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkGeek.DAL.Scripts
{
   public interface IWeatherDAO
   {
        IList<WeatherModel>GetWeather(string parkCode);
        

    }
}
