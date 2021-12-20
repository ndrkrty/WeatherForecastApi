using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApi.Infrastructure.DTO
{
    public class LocationIQServiceResultDto
    {
        public string place_id { get; set; }
        public string licence { get; set; }
        public string osm_type { get; set; }
        public string osm_id { get; set; }
        public List<double> boundingbox { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string display_name { get; set; }
        public string @class { get; set; }
        public string type { get; set; }
        public double importance { get; set; }
        public string icon { get; set; }
    }
}
