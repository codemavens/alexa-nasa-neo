using NasaNeo.Business.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NasaNeo.Business.NasaApi
{
    public static class NasaApiHelper
    {
        public static NasaNeoSet ProcessNeoFromJson(string json, DateTime startDate, DateTime endDate)
        {
            //might want to get the set object here, then for each date, pull the prop from the file like:
            //x.near_earth_objects["2018-05-17"]
            // and let the GetNasaNeoFromJson loop through all of those items and populate a NasaNeoItem
            
            dynamic nasaNeoDynamic = JsonConvert.DeserializeObject(json);

            var result = new NasaNeoSet();

            result.ElementCount = nasaNeoDynamic.element_count.Value;
            result.Links = new NasaNeoLinks()
            {
                Next = nasaNeoDynamic.links.next.Value,
                Prev = nasaNeoDynamic.links.prev.Value,
                Self = nasaNeoDynamic.links.self.Value
            };
            result.ItemsByDate = new List<NasaNeoItemsForDate>();

            var i = nasaNeoDynamic.near_earth_objects.Children();
            //var y = i.Name;
            foreach (var item in i)
            {
                var n = item.Name;
                var allNeosForDate = item.Value;
                result.ItemsByDate.Add(GetNasaNeoFromJson(DateTime.Parse(item.Name), allNeosForDate));

                // we only want the first item because we're only supporting one date
                break;
            }


            //x.links.next.Value
            //"https://api.nasa.gov/neo/rest/v1/feed?start_date=2018-05-18&end_date=2018-05-18&detailed=false&api_key=DEMO_KEY"
            //x.element_count.Value
            //9
            //x.near_earth_objects["2018-05-17"][0].neo_reference_id.Value
            //"3819680"
            //x.near_earth_objects["2018-05-17"].Count
            //9
            //

            throw new NotImplementedException();
        }

        public static NasaNeoItemsForDate GetNasaNeoFromJson(DateTime date, dynamic neosForDate)
        {
            //dynamic x = JsonConvert.DeserializeObject(json);
            //x.links.next.Value
            //"https://api.nasa.gov/neo/rest/v1/feed?start_date=2018-05-18&end_date=2018-05-18&detailed=false&api_key=DEMO_KEY"
            //x.element_count.Value
            //9
            //x.near_earth_objects["2018-05-17"][0].neo_reference_id.Value
            //"3819680"
            //x.near_earth_objects["2018-05-17"].Count
            //9
            //x.near_earth_objects.Children()?

            var result = new NasaNeoItemsForDate()
            {
                Date = date,
                NeoItems = new List<NasaNeoItem>(neosForDate.Count)
            };

            foreach (var item in neosForDate)
            {
                var neo = new NasaNeoItem()
                {
                    AbsoluteMagnitudeH = item.absolute_magnitude_h
                };

                result.NeoItems.Add(neo);
            }

            throw new NotImplementedException();
        }
    }
}
