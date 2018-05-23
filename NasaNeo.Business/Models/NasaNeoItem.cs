using System;

namespace NasaNeo.Business.Models
{
    public class NasaNeoItem
    {
        public NasaNeoLinks Links { get; set; }
        public string ReferenceId { get; set; }
        public string Name { get; set; }
        public string NasaJplUrl { get; set; }
        public double AbsoluteMagnitudeH { get; set; }
        public NasaNeoEstimatedDiameter EstimatedDiameter { get; set; }
        public bool IsPotentiallyHazardous { get; set; }
        public DateTime CloseApproachDate { get; set; }
        public NasaNeoRelativeVelocity RelativeVelocity { get; set; }

        public NasaNeoMissDistance MissDistance { get; set;  }

    }
}
