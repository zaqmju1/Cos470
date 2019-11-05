using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static A3ClassLibrary.JsonLoc;

namespace HaveWeMet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        public ValuesController()
        {
            String dir1 = System.IO.Directory.GetCurrentDirectory();
            dir1 = dir1 + "\\VeryShortHistory.json";
            String dir2 = System.IO.Directory.GetCurrentDirectory();
            dir2 = dir2 + "\\ShortHistory.json";

            locations1 = BuildLocArray(dir1);
            locations2 = BuildLocArray(dir2);
        }

        public Location[] locations1 { set; get; }
        public Location[] locations2 { set; get; }


        // GET HaveMet values
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> GetHaveMet()
        //{
        //    string[] locStr = new string[4];
        //    Location hm = HaveMet(locations1, locations1, 3600000, 20);

        //    locStr[0] = "Most recently location met:";
        //    locStr[1] = "Latitude: " + hm.latitudeE7.ToString();
        //    locStr[2] = "Longitude" + hm.longitudeE7.ToString();
        //    locStr[3] = "Time (ms):" + hm.timestampMs;

        //    return locStr;
        //}

        // GET Alibi values
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAlibi()
        {
            Location[] al = Alibi(locations1, 1548895149104, 1548895390341);
            string[] locStr = new string[(al.Length * 5) + 1];
            int currentNum = 0;

            locStr[0] = "Locations visited:";

            for (int i = 1; i <= locStr.Length - 1; i += 5)
            {
                currentNum = (i + 4) / 5; //

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
