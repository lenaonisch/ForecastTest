using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fiducia.Models
{
    public class ForecastData
    {
        public float MinTemp { get; set; }
        public float MaxTemp { get; set; }
        public float Rain { get; set; }
        public string Summary { get; set; }
    }
}