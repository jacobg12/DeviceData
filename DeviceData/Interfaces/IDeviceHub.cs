using DeviceData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceData.Interfaces
{
    public interface IDeviceHub
    {
        public List<GeneralDeviceData> _generalDeviceData { get; }
        public DateTime GetLastReportedDate();
        public double GetAverageTemperature();
        public long GetTotalRecordCount();
        public double GetAverageHumidity();
    }
}
