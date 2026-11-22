using Sensor.Data;
using Sensor.Reader;
using Sensor.Service;
class Program
{
    static void Main()
    {



        // Console.WriteLine($"{"Sensor",-12} {"Machine",-12} {"Temp",-10} {"Thresh",-10} {"Vib",-10}");
        // Console.WriteLine(new string('-', 52));

        // foreach (var item in sensorData.GetAllSensorData().Take(5))
        // {
        //     Console.WriteLine($"{item.sensor_id,-12} {item.machine_id,-12} {item.temperature,-10} {item.pressure,-10} {item.vibration,-10}");
        // }


        var repo = new SensorRepository("Data/sensor_data.csv");
        var monitoring = new ValidateReading(repo);

        var allSensor = monitoring.GetSensorDataModels();


        Console.WriteLine($"{"Sensor",-12} {"Machine",-12} {"Temp",-10} {"Status",-10}");
        Console.WriteLine(new string('-', 52));

        foreach (var item in allSensor.Take(5))
        {
            string status = monitoring.ClassificationCondition(item);
            
            Console.WriteLine($"{item.sensor_id,-12} {item.machine_id,-12} {item.temperature,-10} {status,-10}");
        }

    }
}
