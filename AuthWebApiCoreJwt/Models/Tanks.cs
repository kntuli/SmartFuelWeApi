using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthWebApiCoreJwt.Models
{
    public class Tanks
    {
        public int UniqueId { get; set; }
        public int SiteID { get; set; }
        public int TankID { get; set; }
        public string SiteName { get; set; }
        public string Manufacturer { get; set; }
        public string ManufacturerCode { get; set; }
        public decimal Capacity { get; set; }
        public decimal Calculated_Volume { get; set; }
        public string Shape { get; set; }
        public string DefaultStrappingTable { get; set; }
        public string ProfileStrappingTable { get; set; }
        public int ATGLogNumber { get; set; }
        public decimal LowLevelLiterAlarm { get; set; }
        public decimal HighLevelLiterAlarm { get; set; }
        public decimal TheftAlarmLiter { get; set; }
        public int TheftAlarmPeriodSec { get; set; }
        public DateTime AtgDateTime { get; set; }
        public DateTime AtgTime { get; set; }
        public DateTime DateTime { get; set; }
        public decimal FuelHeight { get; set; }
        public decimal FuelVolume { get; set; }
        public decimal HBFuelVolume { get; set; }
        public decimal PercentFuelVolume { get; set; }
        public decimal AvgFuelVolume { get; set; }
        public decimal Temp { get; set; }
        public decimal WaterHeight { get; set; }
        public decimal WaterVolume { get; set; }
        public decimal DeltaFuel { get; set; }
        public int DeltaTime { get; set; }
        public int DatabaseID { get; set; }
        public int ForecourtNumber { get; set; }
        public int Forecourt { get; set; }
        public decimal ReorderLevel { get; set; }
        public decimal CurrentLevel { get; set; }
        public decimal CurrentDip { get; set; }
        public string ActionStatus { get; set; }
        public string GUID { get; set; }
        public string Code { get; set; }
        public string Grade { get; set; }

    }
}
