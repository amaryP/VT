using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classTeam2
{
   
    public class RootobjectTeam2
    {
        public api api { get; set; }
     }
    
    public class api
    {
        public int results { get; set; }
        public Team[] teams { get; set; }
    }
    
    public class Team
    {
        public int team_id { get; set; }
        public string name { get; set; }
        public object code { get; set; }
        public string logo { get; set; }
        public string country { get; set; }
        public bool is_national { get; set; }
        public string founded { get; set; }
        public string venue_name { get; set; }
        public string venue_surface { get; set; }
        public string venue_address { get; set; }
        public string venue_city { get; set; }
        public int venue_capacity { get; set; }
    }

}
