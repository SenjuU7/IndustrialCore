using Sensor.ReadingData;
using System.Globalization;

namespace Sensor.MonitoringServices;

public class MonitoringService
{

    private readonly string _filePath;

    public MonitoringService(string filePath = "Data/sensor_data.csv")
    {
        _filePath = filePath;
    }

    // Method untuk membaca seluruh data sensor dari CSV
    public List<SensorReading> GetSensorDataModels()
    {
        if (!File.Exists(_filePath))
        {
            throw new FileNotFoundException($"File CSV tidak ditemukan di: {_filePath}");
        }

        return File.ReadAllLines(_filePath)
            .Skip(1) // Lewati header
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line =>
            {
                var cols = line.Split(',');
                return new SensorReading
                {
                    SensorId = cols[0].Trim(),
                    MachineId = cols[1].Trim(),
                    Temperature = decimal.Parse(cols[2].Trim(), CultureInfo.InvariantCulture),
                    Pressure = decimal.Parse(cols[3].Trim(), CultureInfo.InvariantCulture),
                    Vibration = decimal.Parse(cols[4].Trim(), CultureInfo.InvariantCulture)
                };
            })
            .ToList();
    }

    public List<SensorReading> GetAllSensorData()
    {
        return GetSensorDataModels();
    }

    public string GetMachineStatus(SensorReading data)
    {
        if (data.Temperature > 90m || data.Pressure > 4.5m || data.Vibration > 6m)
        {
            return "CRITICAL";
        }

        if (data.Temperature > 70m || data.Pressure > 3.5m || data.Vibration > 4m)
        {
            return "WARNING";
        }

        return "NORMAL";
    }

}