using NasaNeo.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NasaNeo.Business.NasaApi
{
    public interface INasaNeoRepo
    {
        NasaNeoSet GetNeoForDate(DateTime neoDate);
    }
}
