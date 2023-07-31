using DeviceData.Entities;
using System;
using System.Collections.Generic;

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
