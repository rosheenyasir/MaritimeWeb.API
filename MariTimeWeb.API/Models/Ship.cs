namespace MariTimeWeb.API.Models
{
    public class Ship
    {
        public int ShipId { get; set; }
        public required string Name { get; set; }

        // Navigation property to voyages
        public required ICollection<Voyage> Voyages { get; set; }  // A Ship can have many Voyages
        public required ICollection<VisitedCountry> VisitedCountries { get; set; }  // A Ship can have many VisitedCountries
    }
}
