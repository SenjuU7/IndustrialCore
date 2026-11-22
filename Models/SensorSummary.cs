namespace IndustrialCore.Models;

public record SensorSummary(
    int TotalSensors,
    decimal AverageTemperature,
    int NormalCount,
    int WarningCount,
    int CriticalCount);
