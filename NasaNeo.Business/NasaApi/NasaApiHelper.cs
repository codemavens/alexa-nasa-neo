using NasaNeo.Business.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NasaNeo.Business.NasaApi
{
    public static class NasaApiHelper
    {
        public static NasaNeoSet ProcessNeoFromJson(string json, DateTime date)
        {
            //went with dynamic here instead of JObject because I like the syntax better            
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
            var dateItems = nasaNeoDynamic.near_earth_objects.Children();
            
            foreach (var item in dateItems)
            {
                DateTime itemDate = DateTime.Parse(item.Name);
                if (itemDate == date)
                {
                    var allNeosForDate = item.Value;
                    result.ItemsByDate.Add(GetNasaNeoFromJson(DateTime.Parse(item.Name), allNeosForDate));

                    // we only want the first item because we're only supporting one date for now
                    break;
                }
            }

            return result;
        }

        public static NasaNeoItemsForDate GetNasaNeoFromJson(DateTime date, dynamic neosForDate)
        {
            var result = new NasaNeoItemsForDate()
            {
                Date = date,
                NeoItems = new List<NasaNeoItem>(neosForDate.Count)
            };

            foreach (var item in neosForDate)
            {
                var neo = new NasaNeoItem()
                {
                    Links = new NasaNeoLinks()
                    { 
                        Self = item.links.self,
                    },
                    ReferenceId = item.neo_reference_id,
                    Name = item.name,
                    NasaJplUrl = item.nasa_jpl_url,
                    AbsoluteMagnitudeH = item.absolute_magnitude_h,
                    EstimatedDiameter = new NasaNeoEstimatedDiameter()
                    {
                        KilometersEstimatedMin = item.estimated_diameter.kilometers.estimated_diameter_min,
                        KilometersEstimatedMax = item.estimated_diameter.kilometers.estimated_diameter_max,
                        MetersEstimatedMin = item.estimated_diameter.meters.estimated_diameter_min,
                        MetersEstimatedMax = item.estimated_diameter.meters.estimated_diameter_max,
                        MilesEstimatedMin = item.estimated_diameter.miles.estimated_diameter_min,
                        MilesEstimatedMax = item.estimated_diameter.miles.estimated_diameter_max,
                        FeetEstimatedMin = item.estimated_diameter.feet.estimated_diameter_min,
                        FeetEstimatedMax = item.estimated_diameter.feet.estimated_diameter_max,
                    },
                    IsPotentiallyHazardous = item.is_potentially_hazardous_asteroid,
                    CloseApproachDate = DateTime.Parse(item.close_approach_data[0].close_approach_date.Value),
                    RelativeVelocity = new NasaNeoRelativeVelocity()
                    {
                        KilometersPerSecond = item.close_approach_data[0].relative_velocity.kilometers_per_second,
                        KilometersPerHour = item.close_approach_data[0].relative_velocity.kilometers_per_hour,
                        MilesPerHour = item.close_approach_data[0].relative_velocity.miles_per_hour,
                    },
                    MissDistance = new NasaNeoMissDistance()
                    {
                        Astronomical = item.close_approach_data[0].miss_distance.astronomical,
                        Lunar = item.close_approach_data[0].miss_distance.lunar,
                        Kilometers = item.close_approach_data[0].miss_distance.kilometers,
                        Miles = item.close_approach_data[0].miss_distance.miles,
                    },                    
                };

                result.NeoItems.Add(neo);
            }

            return result;
        }
    }
}
