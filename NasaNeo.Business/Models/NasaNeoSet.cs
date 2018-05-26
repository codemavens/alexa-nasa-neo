using System.Collections.Generic;
using System.Text;

namespace NasaNeo.Business.Models
{
    public class NasaNeoSet
    {
        public NasaNeoLinks Links { get; set; }
        public long ElementCount { get; set;  }
        // For itemsByDate, the json has an object, near_earth_objects, that is an item with 1 prop which is named for the date (like "2018-05-07", and an array for that prop.
        //  Tweak the serialization so that when the element is imported, it parses the date from the property name and 
        public List<NasaNeoItemsForDate> ItemsByDate { get; set; }

    }
}
