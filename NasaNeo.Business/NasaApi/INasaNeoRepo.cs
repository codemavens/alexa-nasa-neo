using NasaNeo.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NasaNeo.Business.NasaApi
{
    public interface INasaNeoRepo
    {
        List<NasaNeoDetails> GetNeoForDateRange(DateTime startDate, DateTime endDate);
    }
}
