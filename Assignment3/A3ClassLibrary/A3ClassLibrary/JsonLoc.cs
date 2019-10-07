using System;
using System.Collections.Generic;

namespace A3ClassLibrary
{
    public class JsonLoc
    {
        public class Loc
        {
            public DateTime time { get; set; }
            public long latitude { get; set; }
            public long longitude { get; set; }
            public Loc(DateTime t, long lat, long lon)
            {
                time = new DateTime(t.Year, t.Month, t.Day, t.Hour, t.Minute, t.Second);
                latitude = lat;
                longitude = lon;
            }

        }


        public static Loc[] Alibi(Loc[] locSet, int year, int month, int day)
        {
            if (locSet == null)
            {
                return null;
            }

            // List variable to accomidate unknown size
            List<Loc> records = new List<Loc>();

            // Search for each location on specified day and add the location to an array
            foreach(Loc x in locSet)
            {
                if (x == null)
                {
                    continue;
                }
                if (x.time.Year == year && x.time.Month == month && x.time.Day == day)
                {
                    records.Add(x);
                }
            }

            // Convert list to our return type
            Loc[] returnRec = records.ToArray();

            return returnRec;
        }


        public static Loc HaveMet(Loc[] locSet1, Loc[] locSet2, int minGapThresh, double meterGapThresh)
        {

            if (locSet1 == null || locSet2 == null)
            {
                return null;
            }

            Loc met = null;
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
                if (locSet1[count1].time.CompareTo(locSet2[count2].time.AddMinutes(minGapThresh)) < 0 && locSet1[count1].time.AddMinutes(minGapThresh).CompareTo(locSet2[count2].time) > 0)
                {
                    // Checks the Latitude and longitude of the two locations to see if they are within a specific range
                    if (locSet1[count1].latitude < locSet2[count2].latitude+meterGapThresh && locSet1[count1].latitude > locSet2[count2].latitude-meterGapThresh)
                    {
                        if (locSet1[count1].longitude < locSet2[count2].longitude + meterGapThresh && locSet1[count1].longitude > locSet2[count2].longitude - meterGapThresh)
                        {
                            // If they have met, set the return variable to the current location of either of the sets
                            met = locSet1[count1];
                            break;
                        }
                    }
                }

                // Iterate the larger list backwards
                if (locSet1[count1].time.CompareTo(locSet2[count2].time) < 0)
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
