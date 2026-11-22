namespace IndustrialCore.Models;

/// <summary>
/// Urutan nilai penting: makin parah makin besar,
/// supaya status mesin bisa diambil lewat Max() dari status tiap parameter.
/// </summary>
public enum MachineStatus
{
    Normal = 0,
    Warning = 1,
    Critical = 2
}
