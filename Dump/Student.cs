
namespace StudentApp.Models;

public class Student
{
    public string Name { get; init; }
    public int[] Scores { get; init; }

    public Student(string name, int[] scores)
    {
        Name = name;
        Scores = scores;
    }
}
