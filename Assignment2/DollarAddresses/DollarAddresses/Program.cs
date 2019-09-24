using System;
using System.Net.Http;
using System.Text;
using System.Web;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DollarAddresses
{
    public class Program
    {

        // Object for the array of addresses
        public class addresses
        {
            public Feature[] features { get; set; }
        }

        // Object for the individual addresses
        public class Feature
        {
            public Attribute attributes { get; set; }
        }

        // Object for the set of attributes related to address
        public class Attribute
        {
            public string STREETNAME { get; set; }
            public string SUFFIX { get; set; }
            public int ADDRESS_NUMBER { get; set; }
        }

        /***AddressValCompare***
         * @int     houseNum
         * @string  street
         * 
         * Compares the street name value (without suffix) against the house number
         * returns: boolean for whether or not the street name value matches the house number
         */
        public static Boolean AddressValCompare(int houseNum, string street)
        {
            int wordVal = 0;
            for (int i = 0; i < street.Length; i++)
            {
                wordVal += (int)Char.ToLower(street[i]) - 96;
            }
            if (wordVal == houseNum)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /***ConfigBuilder***
         *
         * Reads from the config file
         * returns: the object representing the Settings json file
         */
        public static IConfigurationRoot ConfigBuilder()
        {
            

            var config = new ConfigurationBuilder()
				.AddJsonFile("C:\\Users\\Jesse\\source\\repos\\Cos470\\Assignment2\\Settings.json", false, true)
				.Build();
			return config;
        }
        

        /***FindAddresses***
        * @string   where
        * @string   geom
        * @string   spacial
        * @string   outFields
        * @string   orderFields
        * @string   offSet
        * @string   format
        * 
        * Finds all addresses based on the query built
        * returns: void
        */
        public static void FindAddresses(string where, string geom, string spacial, string outFields, string orderFields, string offSet, string format)
        {
            StringBuilder addressString = new StringBuilder();

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["where"] = where;
            query["geometryType"] = geom;
            query["spacialRel"] = spacial;
            query["outFields"] = outFields;
            query["orderByFields"] = orderFields;
            query["resultOffset"] = offSet;
            query["f"] = format;
            var address = @"https://gis.maine.gov/arcgis/rest/services/Location/Maine_E911_Addresses_Roads_PSAP/MapServer/1/query?" + query;

            using (var client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, address))
                {
                    var response = client.SendAsync(request).Result;
                    var content = response.Content.ReadAsStringAsync().Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"{content}: {response.StatusCode}");
                    }
                    var contentJ = JsonConvert.DeserializeObject<addresses>(content);

                    foreach(var f in contentJ.features)
                    {
                        if (AddressValCompare(f.attributes.ADDRESS_NUMBER, f.attributes.STREETNAME) == true)
                        {
                            addressString.Append(f.attributes.ADDRESS_NUMBER + " ");
                            addressString.Append(f.attributes.STREETNAME + " ");
                            addressString.Append(f.attributes.SUFFIX + "\r\n");
                        }
                    }
                    Console.WriteLine(addressString);
                }
            }
        }

        static void Main(string[] args)
        {
            IConfigurationRoot config = ConfigBuilder();
            FindAddresses(config["where"], config["geometryType"], config["spacialRel"], config["outFields"], config["orderByFields"], config["resultOffset"], config["f"]);
        }
    }
}
