using DeviceData.Entities;

namespace DeviceData.Interfaces
{
    public interface IDataLoader
    {
        public IDeviceData1 GetDeviceData1(string path);
        public IDeviceData2 GetDeviceData2(string path);
    }
}
