namespace IndustrialCore.Models;

/// <summary>
/// Satu pembacaan sensor mentah dari sebuah mesin.
/// Satuan: Temperature (°C), Pressure (bar), Vibration (mm/s).
/// </summary>
public class SensorReading
{
    public string SensorId { get; set; } = string.Empty;
    public string MachineId { get; set; } = string.Empty;
    public decimal Temperature { get; set; }   // °C
    public decimal Pressure { get; set; }      // bar
    public decimal Vibration { get; set; }     // mm/s
}
