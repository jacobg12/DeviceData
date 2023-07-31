using DeviceData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceData.Interfaces
{
    public interface IDataLoader
    {
        public DeviceData1 GetDeviceData1();
        public DeviceData2 GetDeviceData2();
    }
}
