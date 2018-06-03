using NasaNeo.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NasaNeo.Business.NasaApi
{
    public interface INasaNeoRepo
    {
        Task<NasaNeoSet> GetNeoForDateAsync(DateTime neoDate);
    }
}
