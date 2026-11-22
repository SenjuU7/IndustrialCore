using Sensor.Overall;

class Program
{
    static void Main()
    {
        SensorService _sensorService = new SensorService();
        _sensorService.RunMonitoring();
    }
}
