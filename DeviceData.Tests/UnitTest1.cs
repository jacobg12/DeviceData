using DeviceData.Entities;
using DeviceData.Factory;
using DeviceData.Interfaces;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Linq;

namespace DeviceData.Tests
{
    public class Tests
    {
        private IDeviceData1 _deviceData1;
        private IDeviceData2 _deviceData2;
        private IDeviceHub _deviceHub;

        private void GeneratePredictableDeviceData1(float seed)
        {
            _deviceData1 = new DeviceData1();
            _deviceData1.PartnerId = 1;
            _deviceData1.PartnerName = "TestCase1";

            float tempIncrementConst = 0.29f;
            //Add Trackers
            int addMinutes = 2;
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

                        crumb.CreatedDtm = DateTime.Parse(tracker.ShipmentStartDtm).AddMinutes(++addMinutes).ToString();
                        crumb.Value = float.Parse(((p * j * ++tempIncrementConst) * (seed+seed)/seed).ToString());

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

                        crumb.CreatedDtm = DateTime.Parse(tracker.ShipmentStartDtm).AddMinutes(++addMinutes).ToString();
                        crumb.Value = float.Parse(((p * j* ++tempIncrementConst) * (seed*seed)).ToString());

                        sensor.Crumbs.Add(crumb);
                    }

                    tracker.Sensors.Add(sensor);
                }
                _deviceData1.Trackers.Add(tracker);
            }
        }
        private void GeneratePredictableDeviceData2(float seed)
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
                    data.Value = float.Parse(((i * j) * seed).ToString());

                    device.SensorData.Add(data);
                }

                for (int j = 2; j < 5; j++)
                {
                    var data = new Sensordata();

                    data.SensorType = "HUM";
                    data.DateTime = DateTime.Parse(device.StartDateTime).AddMinutes(j).ToString();
                    data.Value = float.Parse(((i * j) * seed+seed).ToString());

                    device.SensorData.Add(data);
                }

                _deviceData2.Devices.Add(device);
            }

        }
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(0,6,3)]
        public void DoesReturnCorrectAmountForDevices(double seed,int totalNumber,int eachNumber)
        {
            GeneratePredictableDeviceData1(float.Parse(seed.ToString()));
            GeneratePredictableDeviceData2(float.Parse(seed.ToString()));

            _deviceHub = new DeviceHub(_deviceData1, _deviceData2);

            var items = _deviceHub._generalDeviceData.Select(y => y);


            Assert.AreEqual(items.Count(), totalNumber);

            Assert.AreEqual(items.Where(y=>y.CompanyId == 1).Count(), eachNumber);
            Assert.AreEqual(items.Where(y=>y.CompanyId == 1).Select(x=>x.TemperatureCount).Count(), eachNumber);
            Assert.AreEqual(items.Where(y=>y.CompanyId == 1).Select(x => x.HumidityCount).Count(), eachNumber);

            Assert.AreEqual(items.Where(y=>y.CompanyId == 2).Count(), eachNumber);
            Assert.AreEqual(items.Where(y=>y.CompanyId == 2).Select(x => x.HumidityCount).Count(), eachNumber);
            Assert.AreEqual(items.Where(y=>y.CompanyId == 2).Count(), eachNumber);
        }

        #region Device1
        [TestCase(0.897,0, 111.22d)]
        [TestCase(0.897,1, 435.22d)]
        [TestCase(0.897,2, 759.22d)]
        public void DoesCalulateCorrectTemperatureDevice1(double seed,int index,double value )
        {
            GeneratePredictableDeviceData1(float.Parse(seed.ToString()));
            GeneratePredictableDeviceData2(float.Parse(seed.ToString()));

            _deviceHub = new DeviceHub(_deviceData1, _deviceData2);

            var items = _deviceHub._generalDeviceData.Select(y => y).Where(y=>y.CompanyId == 1);

            Assert.AreEqual(items.ElementAt(index).AverageTemperature, value);
        }

        [TestCase(0.354, 0, 17.119d)]
        [TestCase(0.354, 1, 37.421d)]
        [TestCase(0.354, 2, 57.722d)]
        public void DoesCalulateCorrectHumidityDevice1(double seed, int index, double value)
        {
            GeneratePredictableDeviceData1(float.Parse(seed.ToString()));
            GeneratePredictableDeviceData2(float.Parse(seed.ToString()));

            _deviceHub = new DeviceHub(_deviceData1, _deviceData2);

            var items = _deviceHub._generalDeviceData.Select(y => y).Where(y => y.CompanyId == 1);

            Assert.AreEqual(items.ElementAt(index).AverageHumdity, value);
        }

        [TestCase(0, 0, "1/1/2023 12:03:00 AM")]
        [TestCase(0, 1, "1/1/2023 12:21:00 AM")]
        [TestCase(0, 2, "1/1/2023 12:39:00 AM")]
        public void DoesCalulateCorrectFirstReadDevice1(double seed, int index, string datetime)
        {
            GeneratePredictableDeviceData1(float.Parse(seed.ToString()));
            GeneratePredictableDeviceData2(float.Parse(seed.ToString()));

            _deviceHub = new DeviceHub(_deviceData1, _deviceData2);

            var items = _deviceHub._generalDeviceData.Select(y => y).Where(y => y.CompanyId == 1);


            Assert.AreEqual(items.ElementAt(index).FirstReadingDtm, DateTime.Parse(datetime));
        }


        [TestCase(0, 0, "1/1/2023 12:20:00 AM")]
        [TestCase(0, 1, "1/1/2023 12:38:00 AM")]
        [TestCase(0, 2, "1/1/2023 12:56:00 AM")]
        public void DoesCalulateCorrectLastReadDevice1(double seed, int index, string datetime)
        {
            GeneratePredictableDeviceData1(float.Parse(seed.ToString()));
            GeneratePredictableDeviceData2(float.Parse(seed.ToString()));

            _deviceHub = new DeviceHub(_deviceData1, _deviceData2);

            var items = _deviceHub._generalDeviceData.Select(y => y).Where(y => y.CompanyId == 1);


            var str = items.ElementAt(0).LastReadingDtm.ToString();
            str = items.ElementAt(1).LastReadingDtm.ToString();
            str = items.ElementAt(2).LastReadingDtm.ToString();


            Assert.AreEqual(items.ElementAt(index).LastReadingDtm, DateTime.Parse(datetime));
        }
        #endregion Device1

        #region Device2
        [TestCase(0.897, 0, 5.382d)]
        [TestCase(0.897, 1, 8.073d)]
        [TestCase(0.897, 2, 10.764d)]
        public void DoesCalulateCorrectTemperatureDevice2(double seed, int index, double value)
        {
            GeneratePredictableDeviceData1(float.Parse(seed.ToString()));
            GeneratePredictableDeviceData2(float.Parse(seed.ToString()));

            _deviceHub = new DeviceHub(_deviceData1, _deviceData2);

            var items = _deviceHub._generalDeviceData.Select(y => y).Where(y => y.CompanyId == 2);

            Assert.AreEqual(items.ElementAt(index).AverageTemperature, value);
        }

        [TestCase(0.897, 0, 6.279d)]
        [TestCase(0.897, 1, 8.97d)]
        [TestCase(0.897, 2, 11.661d)]
        public void DoesCalulateCorrectHumidityDevice2(double seed, int index, double value)
        {
            GeneratePredictableDeviceData1(float.Parse(seed.ToString()));
            GeneratePredictableDeviceData2(float.Parse(seed.ToString()));

            _deviceHub = new DeviceHub(_deviceData1, _deviceData2);

            var items = _deviceHub._generalDeviceData.Select(y => y).Where(y => y.CompanyId == 2);

            Assert.AreEqual(items.ElementAt(index).AverageHumdity, value);
        }

        [TestCase(0,0, "1/1/2023 12:04:00 AM")]
        [TestCase(0,1, "1/1/2023 12:05:00 AM")]
        [TestCase(0,2, "1/1/2023 12:06:00 AM")]
        public void DoesCalulateCorrectFirstReadDevice2(double seed,int index, string datetime)
        {
            GeneratePredictableDeviceData1(float.Parse(seed.ToString()));
            GeneratePredictableDeviceData2(float.Parse(seed.ToString()));

            _deviceHub = new DeviceHub(_deviceData1, _deviceData2);

            var items = _deviceHub._generalDeviceData.Select(y => y).Where(y => y.CompanyId == 2);



            Assert.AreEqual(items.ElementAt(index).FirstReadingDtm, DateTime.Parse(datetime)) ;
        }


        [TestCase(0, 0, "1/1/2023 12:06:00 AM")]
        [TestCase(0, 1, "1/1/2023 12:07:00 AM")]
        [TestCase(0, 2, "1/1/2023 12:08:00 AM")]
        public void DoesCalulateCorrectLastReadDevice2(double seed, int index, string datetime)
        {
            GeneratePredictableDeviceData1(float.Parse(seed.ToString()));
            GeneratePredictableDeviceData2(float.Parse(seed.ToString()));

            _deviceHub = new DeviceHub(_deviceData1, _deviceData2);

            var items = _deviceHub._generalDeviceData.Select(y => y).Where(y => y.CompanyId == 2);

            Assert.AreEqual(items.ElementAt(index).LastReadingDtm, DateTime.Parse(datetime));
        }
        #endregion Device2
    }
}