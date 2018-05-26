using System;
using System.Collections.Generic;
using System.Text;
using NasaNeo.Business.Models;
using Newtonsoft.Json;

namespace NasaNeo.Business.NasaApi
{
    public class NasaApiFileRepo : INasaNeoRepo
    {
        public List<NasaNeoSet> GetNeoForDate(DateTime neoDate)
        {
            var fileContents = new System.IO.StreamReader(@"C:\Dev\CodeMavens\Alexa\nasa-neo\sample-data\multi-sample-2018-03-23-to-2018-03-25.json").ReadToEnd();
            //var neoData = JsonConvert.DeserializeObject<object>(fileContents.ReadToEnd());

            var y = NasaApiHelper.ProcessNeoFromJson(fileContents, neoDate);

            throw new NotImplementedException();
        }
    }
}
