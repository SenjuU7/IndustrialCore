using Sensor.SensorServices;

class Program
{
    static void Main()
    {
        SensorService _sensorService = new SensorService();
        _sensorService.RunMonitoring();
    }
}
