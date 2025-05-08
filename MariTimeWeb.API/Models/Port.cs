namespace MariTimeWeb.API.Models
{
    public class Port
    {
        public int PortId { get; set; }
        public required string Name { get; set; }

        // Navigation properties for voyages
        public required ICollection<Voyage> DepartureVoyages { get; set; }  // Ports can have many voyages departing
        public required ICollection<Voyage> ArrivalVoyages { get; set; }    // Ports can have many voyages arriving
    }
}
