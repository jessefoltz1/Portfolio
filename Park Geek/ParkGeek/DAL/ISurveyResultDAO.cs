using ParkGeekMVC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkGeek.DAL
{
    public interface ISurveyResultDAO
    {
        IList<SurveyResultModel> GetAllSurveys();
        int PostSurvey(SurveyResultModel SurveyModel);

    }
}
