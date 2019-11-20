using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkGeek;
using ParkGeek.DAL;
using ParkGeek.DAL.Models;
using ParkGeek.DAL.Scripts;
using ParkGeek.MVC.Models;
using ParkGeekMVC.Models;
using Security.DAO;


namespace ParkGeekMVC.Controllers
{
    public class ParkController : AuthenticationController
    {
        private IWeatherDAO _weatherDAO = null;
        private IParkGeekDAO _parkDAO = null;
        private ISurveyResultDAO _surveyDAO = null;
        private IUserSecurityDAO _userSecurity = null;

        public ParkController(IUserSecurityDAO db, IParkGeekDAO parkDAO, IWeatherDAO weatherDAO, ISurveyResultDAO surveyDAO, IHttpContextAccessor httpContext) : base(db, httpContext)
        {
            _parkDAO = parkDAO;
            _weatherDAO = weatherDAO;
            _surveyDAO = surveyDAO;
            _userSecurity = db;
        }

        public IActionResult Index()
        {
            //get list of parks
            var parks = _parkDAO.GetParks();
            //pass list to VIEW as parameter in get auth view below it
            return GetAuthenticatedView("Index", parks);
            //on the view, bind the type passed in
            //use to build razer

        }
        [HttpGet]
        public IActionResult Details(string parkCode)
        {
            ParkViewModel parkSp = new ParkViewModel();
            //parkSp.ParkCode = parkCode;
            parkSp.Park =_parkDAO.GetPark(parkCode);
            parkSp.Weather = _weatherDAO.GetWeather(parkCode);

            return GetAuthenticatedView("Details", parkSp);
        }
        [HttpGet]
        public IActionResult Survey()
        {
            SurveyResultViewModel SurveyVM = new SurveyResultViewModel();
            SurveyVM.Parks = _parkDAO.GetParks();
            return GetAuthenticatedView("Survey", SurveyVM);

        }

        [HttpPost]
        public IActionResult Survey(SurveyResultModel RM)
        {
            _surveyDAO.PostSurvey(RM);
            return RedirectToAction("Favorites"); 
        }


        [HttpGet]
        public IActionResult Favorites()
        {
            IList<FavoritesViewModel> vmList = new List<FavoritesViewModel>();
            IList<SurveyResultModel> rmList = new List<SurveyResultModel>();
            rmList = _surveyDAO.GetAllSurveys();
            foreach(SurveyResultModel RM in rmList)
            {
                FavoritesViewModel vm = new FavoritesViewModel();
                ParkModel park = _parkDAO.GetPark(RM.ParkCode);
                vm.NumVotes = RM.NumVotes;
                vm.ParkCode = RM.ParkCode;
                vm.ParkName = park.ParkName;
                vm.ParkDesc = park.ParkDescription;
                vmList.Add(vm);

            }
            return View("Favorites", vmList);
        }


        [HttpGet]
        public IActionResult Weather(string parkCode)
        {
            ParkViewModel parkSp = new ParkViewModel();
            parkSp.Weather = _weatherDAO.GetWeather(parkCode);
            return GetAuthenticatedView("Weather", parkSp);
        }
        

        //[HttpGet]
        //public IActionResult Survey()
        //{
        //    SurveyResultViewModel surveySp = new SurveyResultViewModel();
        //    surveySp.ParkCode = parkCode;
        //    surveySp.survey = _surveyDAO.GetAllSurveys(parkCode);

        //    return GetAuthenticatedView("Survey", surveySp);

        //}
        //[HttpPost]
        //public IActionResult Survey(string parkCode)
        //{
        //    SurveyResultViewModel surveySp = new SurveyResultViewModel();
        //    surveySp.ParkCode = parkCode;
        //    surveySp.survey = _surveyDAO.GetAllSurveys(parkCode);

        //    return GetAuthenticatedView("Survey", surveySp);

        //}
    }
}
