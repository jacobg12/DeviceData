using System.Collections.Generic;

namespace DeviceData.Entities
{

    public interface IDeviceData2
    {
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public IList<IDevice> Devices { get; set; }
    }

    public class IDevice
    {
        public int DeviceID { get; set; }
        public string Name { get; set; }
        public string StartDateTime { get; set; }
        public IList<ISensordata> SensorData { get; set; }
    }

    public class ISensordata
    {
        public string SensorType { get; set; }
        public string DateTime { get; set; }
        public float Value { get; set; }
    }

}
