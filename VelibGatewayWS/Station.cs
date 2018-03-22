using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelibGatewayWS
{
   public class Contract
    {
        public String country_code { get; set; }
        public String name { get; set; }
        public List<string> cities { get; set; }
        public String commercial_name { get; set; }
        public bool IsThisContract(String query)
        {
            foreach(string city in cities)
            {
                if (city.Contains(query)) return true;
            }

            return name.Contains(query)|| commercial_name.Contains(query);
        }
    }
   public class Station
    {
        public String name { get; set; }
        public int number { get; set; }
        public int available_bikes { get; set; }
        public bool IsThisStation(String query)
        {
            return name.Contains(query);
        }
    }
}
