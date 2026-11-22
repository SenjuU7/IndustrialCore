namespace Sensor.ReadingData;

public class SensorReading
{
public string SensorId { get; set; } = string.Empty;
public string MachineId { get; set; } = string.Empty;
public decimal Temperature { get; set; }
public decimal Pressure { get; set; }
public decimal Vibration { get; set; }
}

