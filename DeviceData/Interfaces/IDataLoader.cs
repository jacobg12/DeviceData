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
        public IDeviceData1 GetDeviceData1();
        public IDeviceData2 GetDeviceData2();
    }
}
