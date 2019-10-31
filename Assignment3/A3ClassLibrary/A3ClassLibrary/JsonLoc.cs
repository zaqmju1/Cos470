using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace A3ClassLibrary
{
    public class JsonLoc
    {


        public class Loc
        {
            public Location[] locations { get; set; }
        }

        public class Location
        {
            public string timestampMs { get; set; }
            public int latitudeE7 { get; set; }
            public int longitudeE7 { get; set; }
            public int accuracy { get; set; }
            public Activity[] activity { get; set; }
        }

        public class Activity
        {
            public string timestampMs { get; set; }
            public Activity1[] activity { get; set; }
        }

        public class Activity1
        {
            public string type { get; set; }
            public int confidence { get; set; }
        }


        public static Location[] BuildLocArray(String path)
        {
            if (!System.IO.File.Exists(path))
            {
                return null;
            }

            Location[] records;

            Loc locJson = JsonConvert.DeserializeObject<Loc>(System.IO.File.ReadAllText(path));

            records = locJson.locations;

            return records;
        }


        public static long ConvertL(String s)
        {
            return (long)Convert.ToDouble(s);
        }


        public static Location[] Alibi(Location[] locSet, long lowLim, long upLim)
        {
            if (locSet == null)
            {
                return null;
            }

            // List variable to accomidate unknown size
            List<Location> records = new List<Location>();

            // Search for each location on specified day and add the location to an array
            foreach (Location x in locSet)
            {
                if (x == null)
                {
                    continue;
                }
                if (ConvertL(x.timestampMs) < upLim && ConvertL(x.timestampMs) > lowLim)
                {
                    records.Add(x);
                }
            }

            // Convert list to our return type
            Location[] returnRec = records.ToArray();

            return returnRec;
        }


        public static Location HaveMet(Location[] locSet1, Location[] locSet2, int msGapThresh, double distGapThresh)
        {

            if (locSet1 == null || locSet2 == null)
            {
                return null;
            }

            Location met = null;
            int count1 = locSet1.Length - 1;
            int count2 = locSet1.Length - 1;

            // Cycle through both arrays backwards to find the most recent meeting location
            while (count1 >= 0 && count2 >= 0)
            {
                if (locSet1[count1] == null)
                {
                    count1--;
                    continue;
                }
                if (locSet2[count2] == null)
                {
                    count2--;
                    continue;
                }

                // Checks the dates of the two locations to see if they are within a specified range
                if (Math.Abs(ConvertL(locSet1[count1].timestampMs) - ConvertL(locSet2[count2].timestampMs)) < msGapThresh)
                {
                    // Checks the Latitude and longitude of the two locations to see if they are within a specific range
                    if (Math.Abs(locSet1[count1].latitudeE7 - locSet2[count2].latitudeE7) < distGapThresh)
                    {
                        if (Math.Abs(locSet1[count1].longitudeE7 - locSet2[count2].longitudeE7) < distGapThresh)
                        {
                            // If they have met, set the return variable to the current location of either of the sets
                            met = locSet1[count1];
                            break;
                        }
                    }
                }

                // Iterate the larger list backwards
                if (ConvertL(locSet1[count1].timestampMs) < ConvertL(locSet2[count2].timestampMs))
                {
                    count2--;
                }
                else
                {
                    count1--;
                }
            }


            return met;
        }
    }
}
