using System.Collections.Generic;

namespace DeviceData.Entities
{

    public class DeviceData1 : IDeviceData1
    {
        public int PartnerId { get; set; } 
        public string PartnerName { get; set; } = string.Empty;
        public IList<Tracker> Trackers { get; set; } = new List<Tracker>();
    }

    public class Tracker : ITracker
    {
        public int Id { get; set; }
        public string Model { get; set; } = string.Empty;
        public string ShipmentStartDtm { get; set; } = string.Empty;
        public IList<Sensor> Sensors { get; set; } = new List<Sensor>();
    }

    public class Sensor : ISensor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IList<Crumb> Crumbs { get; set; } = new List<Crumb>();
    }

    public class Crumb : ICrumb
    {
        public string CreatedDtm { get; set; } = string.Empty;
        public float Value { get; set; }
    }

}
