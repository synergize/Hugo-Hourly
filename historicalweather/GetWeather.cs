using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace historicalweather
{
    class GetWeather
    {
        public class Coord
        {
            public double Lon { get; set; }
            public double Lat { get; set; }
        }

        public class RootObject
        {
            public Coord Coord { get; set; }
            public string @base { get; set; }
            public int Visibility { get; set; }
            public int Dt { get; set; }
            public int Id { get; set; }
            public string Name { get; set; }
            public int Cod { get; set; }
        }
    }
}
