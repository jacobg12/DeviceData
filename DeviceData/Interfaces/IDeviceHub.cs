using DeviceData.Entities;
using System;
using System.Collections.Generic;

namespace DeviceData.Interfaces
{
    public interface IDeviceHub
    {
        public List<GeneralDeviceData> _generalDeviceData { get; }
        public void PrintSummary();
    }
}
