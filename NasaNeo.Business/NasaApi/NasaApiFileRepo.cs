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
            var fileContents = new System.IO.StreamReader(@"C:\Dev\CodeMavens\Alexa\nasa-neo\sample-data\");
            var neoData = JsonConvert.DeserializeObject<object>(fileContents.ReadToEnd());
            

            throw new NotImplementedException();
        }
    }
}
