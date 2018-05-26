using System;
using System.Collections.Generic;
using System.Text;
using NasaNeo.Business.Models;
using Newtonsoft.Json;

namespace NasaNeo.Business.NasaApi
{
    public class NasaApiFileRepo : INasaNeoRepo
    {
        public List<NasaNeoSet> GetNeoForDateRange(DateTime startDate, DateTime endDate)
        {
            //var fileContents = new System.IO.StreamReader(@"C:\Dev\CodeMavens\Alexa\nasa-neo\sample-data\2018-0517.json").ReadToEnd();
            var fileContents = new System.IO.StreamReader(@"C:\Dev\CodeMavens\Alexa\nasa-neo\sample-data\multi-sample-2018-03-23-to-2018-03-25.json").ReadToEnd();
            //var neoData = JsonConvert.DeserializeObject<object>(fileContents.ReadToEnd());

            //might want to get the set object here, then for each date, pull the prop from the file like:
            //x.near_earth_objects["2018-05-17"]
            // and let the GetNasaNeoFromJson loop through all of those items and populate a NasaNeoItem


            var y = NasaApiHelper.ProcessNeoFromJson(fileContents, startDate, endDate);

            throw new NotImplementedException();
        }
    }
}
