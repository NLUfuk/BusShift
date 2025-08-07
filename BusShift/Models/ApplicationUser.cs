using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusShift.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Guid? DriverId { get; set; }

        [ForeignKey("DriverId")]
        public Driver? Driver { get; set; }

        // Ekstra alan istersen:
        public string? FullName { get; set; }
    }
}
