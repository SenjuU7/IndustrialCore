using StudentApp.Data;
namespace StudentApp.Services;

class StudentService
{
    private readonly StudentRepository _repository = new();

    
    public decimal Avg(int[] currentScore)
    {
        int sum = 0;

        foreach (int score in currentScore)
        {
            sum += score;
        }

        return (decimal)sum / currentScore.Length;
    }

    
    public string Grade(decimal average)
    {
        return average switch
        {
            >= 90 => "A",
            >= 85 => "A-",
            >= 80 => "B",
            >= 75 => "B-",
            >= 70 => "C",
            _ => "F"
        };
    }

    
    public void Overall()
    {
        Console.WriteLine(
            $"{"Student",-15} {"Overall",-15} {"Grade",-15}"
        );

        Console.WriteLine(new string('-', 35));

        foreach (var entry in _repository.Student)
        {
            string studentName = entry.Key;
            int[] currentScore = entry.Value.Scores;

            decimal average = Avg(currentScore);
            string grade = Grade(average);

            Console.WriteLine(
                $"{studentName,-15} {average,-15:F2} {grade,-15}"
            );
        }
    }
}