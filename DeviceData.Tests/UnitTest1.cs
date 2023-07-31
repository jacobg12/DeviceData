using DeviceData.Entities;
using DeviceData.Factory;
using DeviceData.Interfaces;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using NUnit.Framework;
using System;

namespace DeviceData.Tests
{
    public class Tests
    {
        private IDeviceData1 _deviceData1;
        private IDeviceData2 _deviceData2;
        private IDeviceHub _deviceHub;

        private void GeneratePredictableDeviceData1()
        {
            _deviceData1 = new DeviceData1();
            _deviceData1.PartnerId = 1;
            _deviceData1.PartnerName = "TestCase1";

            //Add Trackers
            for (int i = 2; i < 5; i++)
            {
                var tracker = new Tracker();
                tracker.Id = i;
                tracker.Model = string.Format("ModelTest-{0}", i);
                tracker.ShipmentStartDtm = new DateTime(2023, 1, 1).ToString();
                int sensorStart = 0;
                //Add Temp Sensors
                for (int j = 2; j < 5; j++)
                {
                    var sensor = new Sensor();
                    sensor.Id = ++sensorStart;
                    sensor.Name = "Temperature";

                    for (int p = 2; p < 5; p++)
                    {
                        var crumb = new Crumb();

                        crumb.CreatedDtm = DateTime.Parse(tracker.ShipmentStartDtm).AddMinutes(p * j).ToString();
                        crumb.Value = float.Parse(((p * j) * 0.259).ToString());

                        sensor.Crumbs.Add(crumb);
                    }

                    tracker.Sensors.Add(sensor);
                }

                //Add Humidity Sensors
                for (int j = 2; j < 5; j++)
                {
                    var sensor = new Sensor();
                    sensor.Id = ++sensorStart;
                    sensor.Name = "Humidty";

                    for (int p = 2; p < 5; p++)
                    {
                        var crumb = new Crumb();

                        crumb.CreatedDtm = DateTime.Parse(tracker.ShipmentStartDtm).AddMinutes(p * j).ToString();
                        crumb.Value = float.Parse(((p * j) * 0.259).ToString());

                        sensor.Crumbs.Add(crumb);
                    }

                    tracker.Sensors.Add(sensor);
                }
                _deviceData1.Trackers.Add(tracker);
            }
        }
        private void GeneratePredictableDeviceData2()
        {
            _deviceData2 = new DeviceData2();

            _deviceData2.CompanyId = 2;
            _deviceData2.Company = "Testcase2";
            int deviceId = 0;
            for (int i = 2; i < 5; i++)
            {
                var device = new Device();

                device.DeviceID = ++deviceId;
                device.Name = string.Format("DEVICE-{0}", deviceId);
                device.StartDateTime = new DateTime(2023, 1, 1).AddMinutes(i).ToString();

                for(int j = 2;j<5;j++)
                {
                    var data = new Sensordata();

                    data.SensorType = "TEMP";
                    data.DateTime = DateTime.Parse(device.StartDateTime).AddMinutes(j).ToString();
                    data.Value = float.Parse(((i * j) * 0.1898).ToString());

                    device.SensorData.Add(data);
                }

                for (int j = 2; j < 5; j++)
                {
                    var data = new Sensordata();

                    data.SensorType = "HUM";
                    data.DateTime = DateTime.Parse(device.StartDateTime).AddMinutes(j).ToString();
                    data.Value = float.Parse(((i * j) * 0.1098).ToString());

                    device.SensorData.Add(data);
                }

                _deviceData2.Devices.Add(device);
            }

        }
        [SetUp]
        public void Setup()
        {
            GeneratePredictableDeviceData1();
            GeneratePredictableDeviceData2();

            _deviceHub = new DeviceHub(_deviceData1, _deviceData2);



        }

        [TestCase()]
        [TestCase()]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}