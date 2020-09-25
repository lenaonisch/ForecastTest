using System.Threading.Tasks;
using Fiducia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fiducia.Interfaces
{
    public interface IWeatherService
    {
        CityData GetCityData(string name);
        ForecastData GetForecast(CityData city);
    }
}