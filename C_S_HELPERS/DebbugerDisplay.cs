using System.Diagnostics;

[DebuggerDisplay("Name = {FirstName} {LastName}, Born on {FormattedDateOfBirth}")]
public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }

    public string FormattedDateOfBirth => DateOfBirth.ToString("d");
}

class Program
{
    static void Main()
    {
        var person = new Person
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1980, 1, 1)
        };

        Console.WriteLine(person);
    }
}
