using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceData.Entities
{

    public class IDeviceData1
    {
        public int PartnerId { get; set; }
        public string PartnerName { get; set; }
        public IList<ITracker> Trackers { get; set; }
    }

    public class ITracker
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string ShipmentStartDtm { get; set; }
        public IList<ISensor> Sensors { get; set; }
    }

    public class ISensor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Crumb> Crumbs { get; set; }
    }

    public class ICrumb
    {
        public string CreatedDtm { get; set; }
        public float Value { get; set; }
    }

}
