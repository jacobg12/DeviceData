using DeviceData.Entities;
using DeviceData.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace DeviceData.Factory
{
    public class DeviceHub : IDeviceHub
    {
        public IDeviceData1 deviceData1 { get; private set; }
        public IDeviceData2 deviceData2 { get; private set; }

        public List<GeneralDeviceData> _generalDeviceData { get; } = new List<GeneralDeviceData>();


        public DeviceHub(IDeviceData1 data1, IDeviceData2 data2)
        {
            deviceData1 = data1;
            deviceData2 = data2;
            SummerizeDevice1(deviceData1);
            SummerizeDevice2(deviceData2);

            System.IO.File.WriteAllText("MergedList.json", JsonConvert.SerializeObject(_generalDeviceData ,Formatting.Indented));
        }

        public void SummerizeDevice1(IDeviceData1 data)
        {

            foreach (var tracker in data.Trackers)
            {
                GeneralDeviceData generalDeviceData = new GeneralDeviceData(data.PartnerId, data.PartnerName);

                foreach (var sensor in tracker.Sensors)
                {
                    generalDeviceData.DeviceId = tracker.Id;
                    generalDeviceData.DeviceName = tracker.Model;
                    var sorted = sensor.Crumbs.Select(x => x.CreatedDtm).OrderBy(y => DateTime.Parse(y));
                    if (sorted.Count() > 0)
                    {
                        generalDeviceData.FirstReadingDtm = DateTime.Parse(sorted.First());
                        generalDeviceData.LastReadingDtm = DateTime.Parse(sorted.Last());
                    }
                }
                generalDeviceData.TemperatureCount = tracker.Sensors.Where(name => name.Name.ToLower() == "temperature").Count();
                generalDeviceData.HumidityCount = tracker.Sensors.Where(name => name.Name.ToLower() == "humidty").Count();
                generalDeviceData.AverageTemperature = tracker.Sensors.Where(name => name.Name.ToLower() == "temperature").Select(crumbs => crumbs.Crumbs).SelectMany(crumb => crumb).Select(value => value.Value).Average();
                generalDeviceData.AverageHumdity = tracker.Sensors.Where(name => name.Name.ToLower() == "humidty").Select(crumbs => crumbs.Crumbs).SelectMany(crumb => crumb).Select(value => value.Value).Average();

                _generalDeviceData.Add(generalDeviceData);
            }
        }

        public void SummerizeDevice2(IDeviceData2 data)
        {

            foreach (var device in deviceData2.Devices)
            {
                GeneralDeviceData generalDeviceData = new GeneralDeviceData(data.CompanyId, data.Company);
                generalDeviceData.DeviceId = device.DeviceID;
                generalDeviceData.DeviceName = device.Name;
                var sorted = device.SensorData.Select(x => DateTime.Parse(x.DateTime)).OrderBy(x => x);

                if (sorted.Count() > 0)
                {
                    generalDeviceData.FirstReadingDtm = sorted.First();
                    generalDeviceData.LastReadingDtm = sorted.Last();
                }

                generalDeviceData.TemperatureCount = device.SensorData.Where(name => name.SensorType.ToLower() == "temp").Count();
                generalDeviceData.HumidityCount = device.SensorData.Where(name => name.SensorType.ToLower() == "hum").Count();

                generalDeviceData.AverageTemperature = device.SensorData.Where(name => name.SensorType.ToLower() == "temp")
                    .Select(sensordata => sensordata)
                    .Select(value => value.Value).Average();
                generalDeviceData.AverageHumdity = device.SensorData.Where(name => name.SensorType.ToLower() == "hum")
                    .Select(sensordata => sensordata)
                    .Select(value => value.Value).Average();
                _generalDeviceData.Add(generalDeviceData);
            }
        }


        public DateTime GetLastReportedDate()
        {
            throw new NotImplementedException();
        }

        public double GetAverageTemperature()
        {
            throw new NotImplementedException();
        }

        public long GetTotalRecordCount()
        {
            throw new NotImplementedException();
        }

        public double GetAverageHumidity()
        {
            throw new NotImplementedException();
        }

        DateTime IDeviceHub.GetLastReportedDate()
        {
            throw new NotImplementedException();
        }

        double IDeviceHub.GetAverageTemperature()
        {
            throw new NotImplementedException();
        }

        long IDeviceHub.GetTotalRecordCount()
        {
            throw new NotImplementedException();
        }

        double IDeviceHub.GetAverageHumidity()
        {
            throw new NotImplementedException();
        }

    }
}
