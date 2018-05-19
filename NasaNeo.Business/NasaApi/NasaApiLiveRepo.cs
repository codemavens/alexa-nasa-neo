using NasaNeo.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NasaNeo.Business.NasaApi
{
    public class NasaApiLiveRepo : INasaNeoRepo
    {
        public List<NasaNeoDetails> GetNeoForDateRange(DateTime startDate, DateTime endDate)
        {
            var result = new List<NasaNeoDetails>();



            return result;
        }
    }
}
