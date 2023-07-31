using DeviceData.Entities;
using DeviceData.Interfaces;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace DeviceData.Factory
{
    internal class DataLoader : IDataLoader
    {

        public IDeviceData1 GetDeviceData1(string path = "DeviceData.Resources.DeviceDataFoo1.json")
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = path;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                var t = JsonConvert.DeserializeObject<DeviceData1>(result);

                return t;
            }
        }

        public IDeviceData2 GetDeviceData2(string path = "DeviceData.Resources.DeviceDataFoo2.json")
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = path;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                var t = JsonConvert.DeserializeObject<DeviceData2>(result);

                return t;
            }
        }
    }
}
