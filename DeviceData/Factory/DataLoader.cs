using DeviceData.Entities;
using DeviceData.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DeviceData.Factory
{
    internal class DataLoader : IDataLoader
    {

        public IDeviceData1 GetDeviceData1()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "DeviceData.Resources.DeviceDataFoo1.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                var t = JsonConvert.DeserializeObject<DeviceData1>(result);

                return t;
            }
        }

        public IDeviceData2 GetDeviceData2()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "DeviceData.Resources.DeviceDataFoo2.json";

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
