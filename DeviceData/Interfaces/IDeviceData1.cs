﻿using System.Collections.Generic;

namespace DeviceData.Entities
{

    public interface IDeviceData1 
    {
        public int PartnerId { get; set; }
        public string PartnerName { get; set; }
        public IList<Tracker> Trackers { get; set; }
    }

    public interface ITracker
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string ShipmentStartDtm { get; set; }
        public IList<Sensor> Sensors { get; set; }
    }

    public interface ISensor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Crumb> Crumbs { get; set; }
    }

    public interface ICrumb
    {
        public string CreatedDtm { get; set; }
        public float Value { get; set; }
    }

}
