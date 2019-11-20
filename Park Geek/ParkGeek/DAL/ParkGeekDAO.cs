using ParkGeek.DAL.Models;
using ParkGeekMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;


namespace ParkGeek.DAL
{
    public class ParkGeekDAO : IParkGeekDAO
    {
        private string _connectionString;

        public ParkGeekDAO(string connectionString)
        {
            _connectionString = connectionString;
        }


        public IList<ParkModel> GetParks()
        {
            List<ParkModel> parks = new List<ParkModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM park;", conn);


                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    parks.Add(MapRowToPark(reader));
                }
            }

            return parks;
        }
        public ParkModel GetPark(string parkCode)
        {
            ParkModel park = new ParkModel();


            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM park WHERE parkCode = @parkName;", conn);
                cmd.Parameters.AddWithValue("@parkName", parkCode);


                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    park = MapRowToPark(reader);
                }
            }

            return park;
        }

        private ParkModel MapRowToPark(SqlDataReader reader)
        {
            ParkModel result = new ParkModel();

            result.ParkCode = Convert.ToString(reader["parkCode"]);
            result.ParkName = Convert.ToString(reader["parkName"]);
            result.ParkDescription = Convert.ToString(reader["parkDescription"]);
            result.Acerage = Convert.ToInt32(reader["acreage"]);
            result.State = Convert.ToString(reader["state"]);
            result.ElevationFeet = Convert.ToInt32(reader["elevationInFeet"]);
            result.MilesOfTrails = Convert.ToInt32(reader["milesOfTrail"]);
            result.NumOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
            result.Climate = Convert.ToString(reader["climate"]);
            result.YearFounded = Convert.ToInt16(reader["yearFounded"]);
            result.Quote = Convert.ToString(reader["inspirationalQuote"]);
            result.QuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
            result.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
            result.EntryFee = Convert.ToInt16(reader["entryFee"]);
            result.NumOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);
            return result;
        }
    }
}
