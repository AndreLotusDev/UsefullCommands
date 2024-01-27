using System.Diagnostics;

[DebuggerDisplay("Name = {FirstName} {LastName}, Born on {FormattedDateOfBirth}")]
public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }

    public string FormattedDateOfBirth => DateOfBirth.ToString("d");
}

public class BookDB
{
    public string Title { get; set; }
    public string Author { get; set; }

    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public List<string> Tags { get; set; }
}

[DebuggerTypeProxy(typeof(BookTwoView))]
public class BookTwoDb
{
    private const string Teste = "should not appear in debug";
    public string Title { get; set; }
    public string Author { get; set; }
    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public List<string> Tags { get; set; }
    internal class BookTwoView
    {
        public string proxy = "should appear in debug.";
        private BookTwoDb bookTwoDb;
        public BookTwoView(BookTwoDb book)
        {
            this.bookTwoDb = book;
        }
    }
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

        var books = new List<BookDB>
        {
            new BookDB
            {
                Title = "Professional C# 7",
                Author = "Christian Nagel",
                Tags = new List<string> { "Programming", "C#" }
            },
            new BookDB
            {
                Title = "Professional C# 6",
                Author = "Christian Nagel",
                Tags = new List<string> { "Programming", "C#" }
            }
        };

        var booksTwo = new List<BookTwoDb>
        {
            new BookTwoDb
            {
                Title = "Professional C# 7",
                Author = "Christian Nagel",
                Tags = new List<string> { "Programming", "C#" }
            },
            new BookTwoDb
            {
                Title = "Professional C# 6",
                Author = "Christian Nagel",
                Tags = new List<string> { "Programming", "C#" }
            }
        };

        Console.WriteLine(person);
        Console.WriteLine(books);
        Console.WriteLine(booksTwo);
    }
}
