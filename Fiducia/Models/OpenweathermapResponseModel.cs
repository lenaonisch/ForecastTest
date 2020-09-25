using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fiducia.Models
{
    public class Coordinates
    {
        public float lat { get; set; }
        public float lon { get; set; }
    }
    public class CurrentAndForecastResponseModel
    {
        public Coordinates coord { get; set; }
    }
    public class DailyResponseModel
    {
        public List<OneDayForecast> daily { get; set; }
    }

    public class OneDayForecast
    {
        public TemperatureData temp { get; set; }
        public float rain { get; set; }
        public List<WeatherSummary> weather { get; set; }
    }

    public class TemperatureData
    {
        public float min { get; set; }
        public float max { get; set; }

    }

    public class WeatherSummary
    {
        public string main { get; set; }
        public string description { get; set; }
    }

}