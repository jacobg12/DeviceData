using DeviceData.Entities;
using NUnit.Framework;
using System;

namespace DeviceData.Tests
{
    public class Tests
    {
        private DeviceData1 _deviceData1;
        private DeviceData2 _deviceData2;

        [SetUp]
        public void Setup()
        {
            _deviceData1 = new DeviceData1();
            _deviceData2 = new DeviceData2();

            _deviceData1.PartnerId = 0;
            _deviceData1.PartnerName = "TestCase0";

            //Add Tracker
            for (int i = 0; i < 10; i++)
            {
                var tracker = new Tracker();
                tracker.Id = i;
                tracker.Model = string.Format("test-{0}",i);


                _deviceData1.Trackers.Add(tracker);
            }
           
            
        }

        [TestCase()]
        [TestCase()]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}