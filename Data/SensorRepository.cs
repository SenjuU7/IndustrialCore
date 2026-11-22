using System.Globalization;
using IndustrialCore.Models;

namespace IndustrialCore.Data;

/// <summary>
/// Tugas tunggal: membaca data sensor dari CSV dan mengubahnya menjadi SensorReading.
/// Tidak ada business rule di sini.
/// </summary>
public class SensorRepository
{
    private const int ExpectedColumns = 5;
    private readonly string _filePath;

    public SensorRepository(string filePath)
    {
        _filePath = filePath;
    }

    public List<SensorReading> GetAll()
    {
        if (!File.Exists(_filePath))
        {
            throw new FileNotFoundException($"File CSV tidak ditemukan di: {_filePath}");
        }

        var readings = new List<SensorReading>();
        var lines = File.ReadAllLines(_filePath);

        // i = 1 -> lewati header. Nomor baris untuk pesan error = i + 1 (berbasis 1).
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
            {
                continue;
            }

            readings.Add(ParseLine(lines[i], lineNumber: i + 1));
        }

        return readings;
    }

    private static SensorReading ParseLine(string line, int lineNumber)
    {
        var cols = line.Split(',');

        if (cols.Length != ExpectedColumns)
        {
            throw new InvalidDataException(
                $"Baris {lineNumber}: diharapkan {ExpectedColumns} kolom, ditemukan {cols.Length}.");
        }

        return new SensorReading
        {
            SensorId = cols[0].Trim(),
            MachineId = cols[1].Trim(),
            Temperature = ParseDecimal(cols[2], "temperature", lineNumber),
            Pressure = ParseDecimal(cols[3], "pressure", lineNumber),
            Vibration = ParseDecimal(cols[4], "vibration", lineNumber)
        };
    }

    private static decimal ParseDecimal(string raw, string column, int lineNumber)
    {
        if (!decimal.TryParse(raw.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out var value))
        {
            throw new InvalidDataException(
                $"Baris {lineNumber}: nilai '{raw.Trim()}' pada kolom {column} bukan angka yang valid.");
        }

        return value;
    }
}
