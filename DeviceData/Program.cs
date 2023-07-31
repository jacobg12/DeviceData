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
           
            foreach(var analytic in hub._generalDeviceData){
                
                Console.WriteLine(string.Format("Company Id:{0}", analytic.CompanyId));
                Console.WriteLine(string.Format("Company Name:{0}", analytic.CompanyName));
                Console.WriteLine(string.Format("Device Id:{0}", analytic.DeviceId));
                Console.WriteLine(string.Format("Device Name:{0}", analytic.DeviceName));
                Console.WriteLine(string.Format("First Reading:{0}", analytic.FirstReadingDtm));
                Console.WriteLine(string.Format("Last Reading:{0}", analytic.LastReadingDtm));
                Console.WriteLine(string.Format("Temperature Count:{0}", analytic.TemperatureCount));
                Console.WriteLine(string.Format("Average Temperature:{0}", analytic.AverageTemperature));
                Console.WriteLine(string.Format("Humidity Count:{0}", analytic.HumidityCount));
                Console.WriteLine(string.Format("Average Humidity:{0}", analytic.AverageHumdity));
                Console.WriteLine("-----------------------------------------------------------");
            }
            

            
        }
    }
}
