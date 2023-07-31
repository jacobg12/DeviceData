using System.Collections.Generic;

namespace DeviceData.Entities
{

    public class DeviceData1 : IDeviceData1
    {
        public int PartnerId { get; set; } 
        public string PartnerName { get; set; } = string.Empty;
        public IList<ITracker> Trackers { get; set; } = new List<ITracker>();
    }

    public class Tracker : ITracker
    {
        public int Id { get; set; }
        public string Model { get; set; } = string.Empty;
        public string ShipmentStartDtm { get; set; } = string.Empty;
        public IList<ISensor> Sensors { get; set; } = new List<ISensor>();
    }

    public class Sensor : ISensor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IList<ICrumb> Crumbs { get; set; } = new List<ICrumb>();
    }

    public class Crumb : ICrumb
    {
        public string CreatedDtm { get; set; } = string.Empty;
        public float Value { get; set; }
    }

}
