using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApi.Infrastructure.DTO
{
    public class DarkSkyServiceResultDto
    {
        [JsonProperty("latitude")]
        public float Latitude { get; set; }

        [JsonProperty("longitude")]
        public float Longitude { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("currently")]
        public Currently Currently { get; set; }

        [JsonProperty("hourly")]
        public Hourly Hourly { get; set; }

        [JsonProperty("daily")]
        public Daily Daily { get; set; }

        [JsonProperty("flags")]
        public Flags Flags { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }
    }

    public class Currently
    {
        [JsonProperty("time")]
        public int Time { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("precipIntensity")]
        public float PrecipIntensity { get; set; }

        [JsonProperty("precipProbability")]
        public float PrecipProbability { get; set; }

        [JsonProperty("temperature")]
        public float Temperature { get; set; }

        [JsonProperty("apparentTemperature")]
        public float ApparentTemperature { get; set; }

        [JsonProperty("dewPoint")]
        public float DewPoint { get; set; }

        [JsonProperty("humidity")]
        public float Humidity { get; set; }

        [JsonProperty("pressure")]
        public float Pressure { get; set; }

        [JsonProperty("windSpeed")]
        public float WindSpeed { get; set; }

        [JsonProperty("windGust")]
        public float WindGust { get; set; }

        [JsonProperty("windBearing")]
        public int WindBearing { get; set; }

        [JsonProperty("cloudCover")]
        public float CloudCover { get; set; }

        [JsonProperty("uvIndex")]
        public int UvIndex { get; set; }

        [JsonProperty("visibility")]
        public float Visibility { get; set; }

        [JsonProperty("ozone")]
        public float Ozone { get; set; }
    }

    public class Hourly
    {
        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("data")]
        public List<HourlyData> Data { get; set; }
    }

    public class HourlyData
    {
        [JsonProperty("time")]
        public int Time { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("precipIntensity")]
        public float PrecipIntensity { get; set; }

        [JsonProperty("precipProbability")]
        public float PrecipProbability { get; set; }

        [JsonProperty("temperature")]
        public float Temperature { get; set; }

        [JsonProperty("apparentTemperature")]
        public float ApparentTemperature { get; set; }

        [JsonProperty("dewPoint")]
        public float DewPoint { get; set; }

        [JsonProperty("humidity")]
        public float Humidity { get; set; }

        [JsonProperty("pressure")]
        public float Pressure { get; set; }

        [JsonProperty("windSpeed")]
        public float WindSpeed { get; set; }

        [JsonProperty("windGust")]
        public float WindGust { get; set; }

        [JsonProperty("windBearing")]
        public int WindBearing { get; set; }

        [JsonProperty("cloudCover")]
        public float CloudCover { get; set; }

        [JsonProperty("uvIndex")]
        public int UvIndex { get; set; }

        [JsonProperty("visibility")]
        public float Visibility { get; set; }

        [JsonProperty("ozone")]
        public float Ozone { get; set; }
    }

    public class Daily
    {
        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("data")]
        public List<DailyData> Data { get; set; }
    }

    public class DailyData
    {
        [JsonProperty("time")]
        public int Time { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("precipIntensity")]
        public float PrecipIntensity { get; set; }

        [JsonProperty("precipProbability")]
        public float PrecipProbability { get; set; }

        [JsonProperty("temperature")]
        public float Temperature { get; set; }

        [JsonProperty("apparentTemperature")]
        public float ApparentTemperature { get; set; }

        [JsonProperty("dewPoint")]
        public float DewPoint { get; set; }

        [JsonProperty("humidity")]
        public float Humidity { get; set; }

        [JsonProperty("pressure")]
        public float Pressure { get; set; }

        [JsonProperty("windSpeed")]
        public float WindSpeed { get; set; }

        [JsonProperty("windGust")]
        public float WindGust { get; set; }

        [JsonProperty("windBearing")]
        public int WindBearing { get; set; }

        [JsonProperty("cloudCover")]
        public float CloudCover { get; set; }

        [JsonProperty("uvIndex")]
        public int UvIndex { get; set; }

        [JsonProperty("visibility")]
        public float Visibility { get; set; }

        [JsonProperty("ozone")]
        public float Ozone { get; set; }

        [JsonProperty("sunriseTime")]
        public int SunriseTime { get; set; }

        [JsonProperty("sunsetTime")]
        public int SunsetTime { get; set; }

        [JsonProperty("moonPhase")]
        public float MoonPhase { get; set; }

        [JsonProperty("precipIntensityMax")]
        public float PrecipIntensityMax { get; set; }

        [JsonProperty("precipIntensityMaxTime")]
        public int PrecipIntensityMaxTime { get; set; }

        [JsonProperty("precipType")]
        public string PrecipType { get; set; }

        [JsonProperty("temperatureHigh")]
        public float TemperatureHigh { get; set; }

        [JsonProperty("temperatureHighTime")]
        public int TemperatureHighTime { get; set; }

        [JsonProperty("temperatureLow")]
        public float TemperatureLow { get; set; }

        [JsonProperty("temperatureLowTime")]
        public int TemperatureLowTime { get; set; }

        [JsonProperty("apparentTemperatureHigh")]
        public float ApparentTemperatureHigh { get; set; }

        [JsonProperty("apparentTemperatureHighTime")]
        public int ApparentTemperatureHighTime { get; set; }

        [JsonProperty("apparentTemperatureLow")]
        public float ApparentTemperatureLow { get; set; }

        [JsonProperty("apparentTemperatureLowTime")]
        public int ApparentTemperatureLowTime { get; set; }

        [JsonProperty("windGustTime")]
        public int WindGustTime { get; set; }

        [JsonProperty("uvIndexTime")]
        public int UvIndexTime { get; set; }

        [JsonProperty("temperatureMin")]
        public float TemperatureMin { get; set; }

        [JsonProperty("temperatureMinTime")]
        public int TemperatureMinTime { get; set; }

        [JsonProperty("temperatureMax")]
        public float TemperatureMax { get; set; }

        [JsonProperty("temperatureMaxTime")]
        public int TemperatureMaxTime { get; set; }

        [JsonProperty("apparentTemperatureMin")]
        public float ApparentTemperatureMin { get; set; }

        [JsonProperty("apparentTemperatureMinTime")]
        public int ApparentTemperatureMinTime { get; set; }

        [JsonProperty("apparentTemperatureMax")]
        public float ApparentTemperatureMax { get; set; }

        [JsonProperty("apparentTemperatureMaxTime")]
        public int ApparentTemperatureMaxTime { get; set; }
    }

    public class Flags
    {
        [JsonProperty("sources")]
        public List<string> Sources { get; set; }

        [JsonProperty("meteoalarm-license")]
        public string MeteoalarmLicense { get; set; }

        [JsonProperty("nearest-station")]
        public float NearestStation { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }
}
