using System;
using Fiducia.Interfaces;
using Fiducia.Models;

namespace Fiducia.Services
{
    public class OpenweathermapService : IWeatherService
    {
        private string _apiKey;
        private const string _apiURLCurrent = "http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}";
        private const string _apiURLForecast = "http://api.openweathermap.org/data/2.5/onecall?lat={0}&lon={1}&exclude={2}&appid={3}";
        private const string EXCLUDE = "current,minutely,hourly,alerts";

        private RequestService _requestService = new RequestService();

        public OpenweathermapService()
        {
            _apiKey = System.Web.Configuration.WebConfigurationManager.AppSettings["ApiKey"];
        }

        public CityData GetCityData(string name)
        {
            string URL = String.Format(_apiURLCurrent, name, _apiKey);
            var response = _requestService.GetStringResponse(URL);
            var current = _requestService.GetData<CurrentAndForecastResponseModel>(response);
            return new CityData()
            {
                Latitude = current.coord.lat,
                Longtitude = current.coord.lon,
                Name = name
            };
        }

        public ForecastData GetForecast(CityData city)
        {
            
            string URL = String.Format(_apiURLForecast, city.Latitude, city.Longtitude, EXCLUDE, _apiKey);
            var response = _requestService.GetStringResponse(URL);
            var forecast = _requestService.GetData<DailyResponseModel>(response);
            return new ForecastData()
            {
                MaxTemp = forecast.daily[0].temp.max,
                MinTemp = forecast.daily[0].temp.min,
                Rain = forecast.daily[0].rain,
                Summary = forecast.daily[0].weather[0].description
            };
        }
    }
}