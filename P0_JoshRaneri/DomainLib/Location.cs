using System.ComponentModel.DataAnnotations;

namespace DomainLib
{
    public class Location
    {
        public Location()
        {            
        }
        public Location(string locName)
        {
            this.LocationName = locName;
        }
        private int locationId;
        [Key]
        public int LocationId { get => locationId; set => locationId = value; }
        [Required]
        private string locationName;
        public string LocationName { get => locationName; set => locationName = value; }
    }
}