
using StudentApp.Models;
using StudentApp.Services;


class Program
{
    static void Main()
    {
        StudentService data = new StudentService();

        Console.WriteLine(
            $"{"Student",-15} {"Overall",-15} {"Grade",-18}"
        );

        data.Overall();


    }
}
