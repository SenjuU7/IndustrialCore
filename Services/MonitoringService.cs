using Sensor.Data;
using System.Globalization;

namespace Sensor.Service;

public class MonitoringService
{

    private readonly string _filePath;

    public MonitoringService(string filePath = "Data/sensor_data.csv")
    {
        _filePath = filePath;
    }

    // Method untuk membaca seluruh data sensor dari CSV
    public List<SensorDataModel> GetSensorDataModels()
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
                return new SensorDataModel
                {
                    sensor_id = cols[0].Trim(),
                    machine_id = cols[1].Trim(),
                    temperature = decimal.Parse(cols[2].Trim(), CultureInfo.InvariantCulture),
                    pressure = decimal.Parse(cols[3].Trim(), CultureInfo.InvariantCulture),
                    vibration = decimal.Parse(cols[4].Trim(), CultureInfo.InvariantCulture)
                };
            })
            .ToList();
    }

    public List<SensorDataModel> GetAllSensorData()
    {
        return GetSensorDataModels();
    }

    public string GetMachineStatus(SensorDataModel data)
    {
        if (data.temperature > 90m || data.pressure > 4.5m || data.vibration > 6m)
        {
            return "CRITICAL";
        }

        if (data.temperature > 70m || data.pressure > 3.5m || data.vibration > 4m)
        {
            return "WARNING";
        }

        return "NORMAL";
    }

}