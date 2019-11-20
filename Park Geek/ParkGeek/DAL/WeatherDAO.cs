using ParkGeek.DAL.Models;
using ParkGeek.DAL.Scripts;
using ParkGeekMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ParkGeek.DAL
{
    public class WeatherDAO : IWeatherDAO
    {
        
        
            private string _connectionString;

            public WeatherDAO(string connectionString)
            {
                _connectionString = connectionString;
            }


            public IList<WeatherModel> GetAllWeather()
            {
                List<WeatherModel> weather = new List<WeatherModel>();

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM weather;", conn);


                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        weather.Add(MapRowToWeather(reader));
                    }
                }

                return weather;
            }
            public IList <WeatherModel> GetWeather(string parkCode)
            {
            List<WeatherModel> weather = new List<WeatherModel>();


                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM weather WHERE parkCode = @parkCode ORDER BY fiveDayForecastValue;", conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);


                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        weather.Add (MapRowToWeather(reader));
                    }
                }

                return weather;
            }

            private WeatherModel MapRowToWeather(SqlDataReader reader)
            {
                return new WeatherModel()
                {
                    ParkCode = Convert.ToString(reader["parkCode"]),
                    FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]),
                    LowTemp = Convert.ToInt32(reader["low"]),
                    HighTemp = Convert.ToInt32(reader["high"]),
                    ForeCast = Convert.ToString(reader["forecast"]),

                };
            }

        
    }
}
