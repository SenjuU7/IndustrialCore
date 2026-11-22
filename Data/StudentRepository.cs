
using StudentApp.Models;

namespace StudentApp.Data;

public class StudentRepository
{
    public Dictionary<string, Student> Student { get; } = new()
    {
        ["Sophia"] = new Student(
            "Sophia",
            new[] { 90, 86, 87, 98, 100, 94, 90 }
        ),

        ["Andrew"] = new Student(
            "Andrew",
            new[] { 89, 78, 89, 92, 90, 90, 89 }
        ),

        ["Emma"] = new Student(
            "Emma",
            new[] { 90, 85, 87, 98, 68, 89, 89 }
        ),

        ["Logan"] = new Student(
            "Logan",
            new[] { 96, 96, 96, 96, 96, 96 }
        )
    };
}
