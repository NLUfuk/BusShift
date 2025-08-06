namespace BusShift.Models
{
    public class BusStation : BaseEntity
    {
        public required string Name { get; set; }
        public required string Location { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string? Notes { get; set; }
      
    }
}
