using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static A3ClassLibrary.JsonLoc;

namespace HaveWeMet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        public class Configs
        {
            public int msThreshold { get; set; }
            public int distThreshold { get; set; }
            public long msLowerLimit { get; set; }
            public long msUpperLimit { get; set; }
        }

        public ValuesController()
        {
            String dir = System.IO.Directory.GetCurrentDirectory();
            String file1Loc = dir + "\\VeryShortHistory.json";
            String file2Loc = dir + "\\ShortHistory.json";

            cfig = JsonConvert.DeserializeObject<Configs>(System.IO.File.ReadAllText(dir + "\\Settings.json"));

            locations1 = BuildLocArray(file1Loc);
            locations2 = BuildLocArray(file2Loc);
        }

        public Location[] locations1 { set; get; }
        public Location[] locations2 { set; get; }
        public Configs cfig { set; get; }


        // GET HaveMet values
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> GetHaveMet()
        //{
        //    string[] locStr = new string[4];
        //    Location hm = HaveMet(locations1, locations1, cfig.msThreshold, cfig.distThreshold);

        //    locStr[0] = "Most recently location met:";
        //    locStr[1] = "Latitude: " + hm.latitudeE7.ToString();
        //    locStr[2] = "Longitude:" + hm.longitudeE7.ToString();
        //    locStr[3] = "Time (ms):" + hm.timestampMs;

        //    return locStr;
        //}

        // GET Alibi values
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAlibi()
        {
            Location[] al = Alibi(locations1, cfig.msLowerLimit, cfig.msUpperLimit);
            string[] locStr = new string[(al.Length * 5) + 1];
            int currentNum = 0;

            locStr[0] = "Locations visited:";

            // Reads each line
            for (int i = 1; i <= locStr.Length - 1; i += 5)
            {
                currentNum = (i + 4) / 5;   // Updated count of the current location object

                locStr[i] = "location: " + (currentNum).ToString();
                locStr[i + 1] = "Latitude: " + al[currentNum - 1].latitudeE7.ToString();
                locStr[i + 2] = "Longitude: " + al[currentNum - 1].longitudeE7.ToString();
                locStr[i + 3] = "Time (ms): " + al[currentNum - 1].timestampMs.ToString();
                locStr[i + 4] = " ";


            }

            return locStr;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
