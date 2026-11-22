using IndustrialCore.Data;
using IndustrialCore.Services;

// Path relatif ke lokasi executable, bukan working directory.
var csvPath = Path.Combine(AppContext.BaseDirectory, "Data", "sensor_data.csv");

var repository = new SensorRepository(csvPath);
var sensorService = new SensorService();
var monitoring = new MonitoringService(repository, sensorService);

monitoring.Run();
