using System;
using System.Collections.Generic;

namespace NasaNeo.Business.Models
{
    public class NasaNeoItemsForDate
    {
        public DateTime Date { get; set; }
        public List<NasaNeoItem> NeoItems { get; set;  }
    }
}
