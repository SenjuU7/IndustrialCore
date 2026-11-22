using Sensor.Data;
using Sensor.Service;

namespace Sensor.Overall;

public class SensorService
{
    public void RunMonitoring()
    {

        var monitoring = new MonitoringService("Data/sensor_data.csv");

        var allSensor = monitoring.GetAllSensorData();

        Console.WriteLine($"{"Sensor",-12} {"Machine",-12} {"Temp",-10} {"Status",-10}");
        Console.WriteLine(new string('-', 52));

        var top5Hottest = allSensor
        .OrderByDescending(item => item.temperature)
        .Take(5);

        foreach (var item in top5Hottest)
        {
            string status = monitoring.GetMachineStatus(item);

            Console.WriteLine($"{item.sensor_id,-12} {item.machine_id,-12} {item.temperature,-10} {status,-10}");
        }
    }
}