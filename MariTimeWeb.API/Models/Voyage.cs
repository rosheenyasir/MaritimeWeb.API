using System;

namespace MariTimeWeb.API.Models
{
    public class Voyage
    {
        public int VoyageId { get; set; }
        public DateTime Date { get; set; }

        public int ShipId { get; set; }
        public required Ship Ship { get; set; }

        public int DeparturePortId { get; set; }
        public required Port DeparturePort { get; set; }

        public int ArrivalPortId { get; set; }
        public required Port ArrivalPort { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
