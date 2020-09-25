using System;
using System.Web.Mvc;
using Fiducia.Interfaces;
using Fiducia.Services;

namespace Fiducia.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        IWeatherService _weatherService;
        private const string COOKIE_LAST_CITY_NAME = "lastCity";
        private const string ALERT_MESSAGE = "It is rainy today!";
        private const string ALERT_KEYWORD = "rain";

        public ActionResult Index()
        {
            if (HttpContext.Request.Cookies[COOKIE_LAST_CITY_NAME] != null)
            {
                ViewBag.LastCity = HttpContext.Request.Cookies[COOKIE_LAST_CITY_NAME].Value;
            }
            return View();
        }
        
        [HttpPost]
        public ActionResult Index(string cityName)
        {
            _weatherService = new OpenweathermapService();
            var cityData = _weatherService.GetCityData(cityName);
            var forecast = _weatherService.GetForecast(cityData);
            bool rainWarn = forecast.Summary.ToLower().Contains(ALERT_KEYWORD);
            SaveUserData(cityName, ref rainWarn);

            ViewBag.Forecast = forecast;
            ViewBag.AlertMessage = rainWarn ? ALERT_MESSAGE: null;
            
            return View();
        }

        private void SaveUserData(string cityName, ref bool rainWarning)
        {
            HttpContext.Response.Cookies[COOKIE_LAST_CITY_NAME].Value = cityName;

            if (rainWarning)
            {
                string cityDateWarningKey = cityName + DateTime.Now.Date.ToShortDateString();

                //set cookie if no warning today. 
                //SHOULD BE IMPLEMENTED via CACHE!!!!
                if (HttpContext.Request.Cookies[cityDateWarningKey] == null)
                {
                    HttpContext.Response.Cookies[cityDateWarningKey].Value = "true";
                }
                else
                {
                    rainWarning = false;
                }
            }
        }
    }
}