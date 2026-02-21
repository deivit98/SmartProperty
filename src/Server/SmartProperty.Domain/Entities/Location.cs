using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmartProperty.Domain.Entities
{
    public class Location
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(300)]
        public string Address { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [MaxLength(100)]
        public string State { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Country { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? ZipCode { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Neighborhood { get; set; }

        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
