namespace Sensor.Data;

public class SensorReading
{
    public string sensor_id { get; set; } = string.Empty;
    public string machine_id { get; set; } = string.Empty;
    public decimal temperature { get; set; }
    public decimal pressure { get; set; }
    public decimal vibration { get; set; }
}

