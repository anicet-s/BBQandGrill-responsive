using System;

namespace BBQandGrill.Models
{
    /// <summary>
    /// Represents a restaurant location
    /// </summary>
    public class Location
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public TimeSpan? OpeningTime { get; set; }
        public TimeSpan? ClosingTime { get; set; }
        public bool IsActive { get; set; }

        public string FullAddress
        {
            get
            {
                return $"{Address}, {City}, {State} {ZipCode}";
            }
        }

        public string FormattedHours
        {
            get
            {
                if (OpeningTime.HasValue && ClosingTime.HasValue)
                {
                    return $"{OpeningTime.Value:hh\\:mm} - {ClosingTime.Value:hh\\:mm}";
                }
                return "Hours not available";
            }
        }
    }
}
