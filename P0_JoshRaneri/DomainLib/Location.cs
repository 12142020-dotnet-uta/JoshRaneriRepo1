using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int LocationId { get => locationId; set => locationId = value; }
        [Required]
        private string locationName;
        public string LocationName { get => locationName; set => locationName = value; }
    }
}