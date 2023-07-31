using System.Collections.Generic;

namespace DeviceData.Entities
{

    public class DeviceData2 : IDeviceData2
    {
        public int CompanyId { get; set; } 
        public string Company { get; set; } = string.Empty;
        public IList<IDevice> Devices { get; set; } = new List<IDevice>();
    }

    public class Device : IDevice
    {
        public int DeviceID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string StartDateTime { get; set; } = string.Empty;
        public IList<ISensordata> SensorData { get; set; } = new List<ISensordata>();
    }

    public class Sensordata : ISensordata
    {
        public string SensorType { get; set; } = string.Empty;
        public string DateTime { get; set; } = string.Empty;
        public float Value { get; set; }
    }

}
