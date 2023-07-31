using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceData.Entities
{

    public class DeviceData1 : IDeviceData1
    {
        public int PartnerId { get; set; }
        public string PartnerName { get; set; }
        public List<Tracker> Trackers { get; set; }
    }

    public class Tracker
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string ShipmentStartDtm { get; set; }
        public List<Sensor> Sensors { get; set; }
    }

    public class Sensor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Crumb> Crumbs { get; set; }
    }

    public class Crumb
    {
        public string CreatedDtm { get; set; }
        public float Value { get; set; }
    }

}
