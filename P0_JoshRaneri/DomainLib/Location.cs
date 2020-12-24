using System;
using System.ComponentModel.DataAnnotations;

namespace P0_JoshRaneri
{
    public class Location
    {
        private Guid locationId = Guid.NewGuid();
        [Key]
        public Guid LocationId { get { return locationId; } set { locationId = value; } }
    }
}