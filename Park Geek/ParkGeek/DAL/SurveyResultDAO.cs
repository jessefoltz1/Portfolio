using ParkGeekMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ParkGeek.DAL
{
    public class SurveyResultDAO: ISurveyResultDAO
    {
        
        
        private string _connectionString;

        public SurveyResultDAO(string connectionString)
        {
            _connectionString = connectionString;
        }


        public IList<SurveyResultModel> GetAllSurveys()
        {
            
                List<SurveyResultModel> surveys = new List<SurveyResultModel>(); 

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT parkCode, COUNT(*) AS numVotes FROM survey_result GROUP BY parkCode ORDER BY numVotes DESC", conn);


                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SurveyResultModel rm = new SurveyResultModel();
                    rm.ParkCode = Convert.ToString(reader["parkCode"]);
                    rm.NumVotes = Convert.ToInt32(reader["numVotes"]);
                    surveys.Add(rm);
                }
            }

            return surveys;
        }

        private SurveyResultModel MapRowToSurveys(SqlDataReader reader)
        {
            return new SurveyResultModel()
            {
                SurveyID = Convert.ToInt32(reader["surveyId"]),
                ParkCode = Convert.ToString(reader["parkCode"]),
                Email = Convert.ToString(reader["email"]),
                State = Convert.ToString(reader["state"]),
                ActivityLevel = Convert.ToString(reader["activityLevel"]),
                NumVotes = Convert.ToInt32(reader["numVotes"])
            };
        }


        public int PostSurvey(SurveyResultModel surveyModel)
        {
            int result = 0;


            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO survey_result (parkCode, emailAddress, state, activityLevel) " +
                    "VALUES ( @parkCode, @emailAddress, @state, @activityLevel) SELECT CAST(SCOPE_IDENTITY() as int)", conn);
                cmd.Parameters.AddWithValue("@parkCode", surveyModel.ParkCode);
                cmd.Parameters.AddWithValue("@emailAddress", surveyModel.Email);
                cmd.Parameters.AddWithValue("@state", surveyModel.State);
                cmd.Parameters.AddWithValue("@activityLevel", surveyModel.ActivityLevel);
                result = (int)cmd.ExecuteScalar();
            }

            return result;
        }
        
    }
}
