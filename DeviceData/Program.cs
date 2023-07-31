using DeviceData.Factory;
using System;

namespace DeviceData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataLoader loader = new DataLoader();   
            DeviceHub hub = new DeviceHub(loader.GetDeviceData1(),loader.GetDeviceData2());

            hub.PrintSummary();
        }
    }
}
