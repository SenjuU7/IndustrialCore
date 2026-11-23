using IndustrialCore.Models;

namespace IndustrialCore.Services;

/// <summary>
/// Berisi seluruh business rule: validasi dan klasifikasi (sesuai TASK.md).
/// Semua method di sini tidak punya side effect (tidak I/O, tidak print).
/// </summary>
public class SensorService
{
    // Threshold: nilai di ATAS batas dianggap naik level.
    private const decimal TemperatureWarning = 70m;
    private const decimal TemperatureCritical = 90m;

    private const decimal PressureWarning = 3m;
    private const decimal PressureCritical = 4m;

    private const decimal VibrationWarning = 3m;
    private const decimal VibrationCritical = 6m;

    public bool ValidateReading(SensorReading? reading)
    {
        if (reading is null) return false;
        if (string.IsNullOrWhiteSpace(reading.SensorId)) return false;
        if (string.IsNullOrWhiteSpace(reading.MachineId)) return false;

        return reading.Temperature >= 0m
            && reading.Pressure >= 0m
            && reading.Vibration >= 0m;
    }

    /// <summary>Rata-rata suhu (°C). Mengembalikan 0 jika tidak ada data.</summary>
    public decimal CalculateAverageTemperature(IReadOnlyCollection<SensorReading> readings)
    {
        return readings.Count == 0 ? 0m : readings.Average(r => r.Temperature);
    }

    // Satu-satunya tempat logika "bandingkan dengan batas", dipakai semua parameter.
    private static MachineStatus Classify(decimal value, decimal warning, decimal critical)
    {
        if (value > critical) return MachineStatus.Critical;
        if (value > warning) return MachineStatus.Warning;
        return MachineStatus.Normal;
    }

    public MachineStatus GetTemperatureStatus(decimal temperature) =>
        Classify(temperature, TemperatureWarning, TemperatureCritical);

    public MachineStatus GetPressureStatus(decimal pressure) =>
        Classify(pressure, PressureWarning, PressureCritical);

    public MachineStatus GetVibrationStatus(decimal vibration) =>
        Classify(vibration, VibrationWarning, VibrationCritical);

    /// <summary>
    /// Status mesin = status terburuk dari ketiga parameter.
    /// Tidak mengulang threshold: hanya memakai tiga method klasifikasi di atas.
    /// </summary>
    public MachineStatus GetMachineStatus(SensorReading reading)
    {
        var temperature = GetTemperatureStatus(reading.Temperature);
        var pressure = GetPressureStatus(reading.Pressure);
        var vibration = GetVibrationStatus(reading.Vibration);

        return (MachineStatus)Math.Max((int)temperature, Math.Max((int)pressure, (int)vibration));
    }

    public List<string> GetHighRiskSensorIds(IEnumerable<SensorReading> readings)
    {
        return readings
        .Where(r => r.Temperature > TemperatureCritical && r.Vibration > VibrationCritical)
        .Select(r => r.SensorId)
        .ToList();
    }

}
