namespace MariTimeWeb.API.Models
{
    public class VisitedCountry
    {
        public int VisitId { get; set; }
        public int VisitedCountryId { get; set; }
        public int ShipId { get; set; }
        public required string Country { get; set; }

        // Navigation property to Ship
        public required Ship Ship { get; set; }
    }
}
