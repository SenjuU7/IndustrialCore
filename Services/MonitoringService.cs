using IndustrialCore.Data;
using IndustrialCore.Models;

namespace IndustrialCore.Services;

/// <summary>
/// Orkestrasi: ambil data -> validasi -> klasifikasi -> hitung ringkasan -> tampilkan.
/// </summary>
public class MonitoringService
{
    private readonly SensorRepository _repository;
    private readonly SensorService _sensorService;

    public MonitoringService(SensorRepository repository, SensorService sensorService)
    {
        _repository = repository;
        _sensorService = sensorService;
    }

    public SensorSummary BuildSummary(IReadOnlyCollection<SensorReading> readings)
    {
        var statuses = readings.Select(_sensorService.GetMachineStatus).ToList();

        return new SensorSummary(
            TotalSensors: readings.Count,
            AverageTemperature: _sensorService.CalculateAverageTemperature(readings),
            NormalCount: statuses.Count(s => s == MachineStatus.Normal),
            WarningCount: statuses.Count(s => s == MachineStatus.Warning),
            CriticalCount: statuses.Count(s => s == MachineStatus.Critical));
    }

    public void Run()
    {
        var all = _repository.GetAll();
        var valid = all.Where(_sensorService.ValidateReading).ToList();
        int skipped = all.Count - valid.Count;

        // PrintTable(valid);
        // PrintSummary(BuildSummary(valid), skipped);
        // PrintHighRiskSensorIds(valid);
    }

    private void PrintTable(IEnumerable<SensorReading> readings)
    {
        Console.WriteLine($"{"Sensor",-8} {"Machine",-9} {"Temp(°C)",10} {"Press(bar)",11} {"Vib(mm/s)",10}  {"Status",-8}");
        Console.WriteLine(new string('-', 62));

        foreach (var r in readings.Take(5))
        {
            var status = _sensorService.GetMachineStatus(r).ToString().ToUpperInvariant();
            Console.WriteLine($"{r.SensorId,-8} {r.MachineId,-9} {r.Temperature,10:0.0} {r.Pressure,11:0.0} {r.Vibration,10:0.0}  {status,-8}");
        }
    }

    private static void PrintSummary(SensorSummary s, int skipped)
    {
        Console.WriteLine();
        Console.WriteLine($"Jumlah sensor      : {s.TotalSensors}");
        Console.WriteLine($"Rata-rata suhu     : {s.AverageTemperature:0.00} °C");
        Console.WriteLine($"Status NORMAL      : {s.NormalCount}");
        Console.WriteLine($"Status WARNING     : {s.WarningCount}");
        Console.WriteLine($"Status CRITICAL    : {s.CriticalCount}");
        Console.WriteLine();

        if (skipped > 0)
        {
            Console.WriteLine($"Data tidak valid dilewati: {skipped}");
        }
    }

    private void PrintHighRiskSensorIds(IEnumerable<SensorReading> r)
    {
        var ids = _sensorService.GetHighRiskSensorIds(r);
        System.Console.WriteLine($"Sensor berisiko tinggi: {string.Join(", ", ids)}");
    }
}
