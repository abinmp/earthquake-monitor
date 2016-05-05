using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Earthquakes
{
    public class Earthquake
    {
        public string Place { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Magnitude { get; set; }
        public string NearestCities { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal Depth { get; set; }
    }
}
