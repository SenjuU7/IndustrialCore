using Sensor.Data;
using Sensor.Reader;


namespace Sensor.Service;

public class ValidateReading
{
    private readonly SensorRepository _repository;
    
    public ValidateReading(SensorRepository repository)
    {
        _repository = repository;
    }

    public List<SensorDataModel> GetSensorDataModels()
    {
        return _repository.GetAllSensorData();
    }

    public string ClassificationCondition(SensorDataModel data)
    {
        if (data.temperature > 90m || data.pressure > 4.5m || data.vibration > 6m)
        {
            return "CRITICAL";
        }

        if (data.temperature > 70m || data.pressure > 3.5m || data.vibration > 4m)
        {
            return "WARNING";
        }

        return "NORMAL";
    }
    
}