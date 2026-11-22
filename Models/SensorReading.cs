using System.Globalization;
using Sensor.Data;

namespace Sensor.Reader;

public class SensorRepository
{
    private readonly string _filePath;

    // Constructor menerima path file (default: Data/sensor_data.csv)
    public SensorRepository(string filePath = "Data/sensor_data.csv")
    {
        _filePath = filePath;
    }

    // Method untuk membaca seluruh data sensor dari CSV
    public List<SensorDataModel> GetAllSensorData()
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
}