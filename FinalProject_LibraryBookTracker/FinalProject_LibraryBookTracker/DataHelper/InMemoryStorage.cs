using FinalProject_LibraryBookTracker.Model;
using static FinalProject_LibraryBookTracker.Model.Report;

namespace FinalProject_LibraryBookTracker.DataHelper
{
    public static class InMemoryStorage
    {
        public static string salt = ")GN#447#^nryrETNwrbR%#&NBRE%#%BBDT#%";

        // Lists to store Books, Users, Loans, and Reports
        public static List<Book> Books = new List<Book>();
        public static List<Patron> Patrons = new List<Patron>
        {
             // Seed
            new Patron
        {
            PatronId = 1,
            Username = "admin",
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "dde6a80ee27ed148c86020e2cfaf6d28", // Presuming this is the hashed password
            PhoneNumber = "+1234567890",
            Address = "123 Elm Street, Springfield, IL",
            MemberSince = new DateTime(2020, 5, 1),
            Role = "Patron",
            Loans = new List<Loan> { new Loan { LoanId = 1, BookId = 1, LoanDate = DateTime.Now.AddDays(-10), ReturnDate = DateTime.Now.AddDays(10) } }
        },
        new Patron
        {
            PatronId = 2,
            Username = "Jane",
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane.smith@example.com",
            Password = "6cb75f652a9b52798eb6cf2201057c73", // Presuming this is the hashed password
            PhoneNumber = "+1987654321",
            Address = "456 Oak Avenue, Springfield, IL",
            MemberSince = new DateTime(2021, 3, 15),
            Role = "Patron",
            Loans = new List<Loan> { new Loan { LoanId = 2, BookId = 2, LoanDate = DateTime.Now.AddDays(-5), ReturnDate = DateTime.Now.AddDays(15) } }
        },
        new Patron
        {
            PatronId = 3,
            Username = "Mike",
            FirstName = "Mike",
            LastName = "Johnson",
            Email = "mike.johnson@example.com",
            Password = "819b0643d6b89dc9b579fdfc9094f28e", // Presuming this is the hashed password
            PhoneNumber = "+1239876543",
            Address = "789 Pine Road, Springfield, IL",
            MemberSince = new DateTime(2022, 1, 10),
            Role = "Patron",
            Loans = new List<Loan> { new Loan { LoanId = 3, BookId = 3, LoanDate = DateTime.Now.AddDays(-3), ReturnDate = DateTime.Now.AddDays(7) } }
        },
        new Patron
        {
            PatronId = 4,
            Username = "Lisa",
            FirstName = "Lisa",
            LastName = "Brown",
            Email = "lisa.brown@example.com",
            Password = "34cc93ece0ba9e3f6f235d4af979b16c", // Presuming this is the hashed password
            PhoneNumber = "+1472583690",
            Address = "101 Maple Drive, Springfield, IL",
            MemberSince = new DateTime(2023, 2, 25),
            Role = "Patron",
            Loans = new List<Loan> { new Loan { LoanId = 4, BookId = 4, LoanDate = DateTime.Now.AddDays(-1), ReturnDate = DateTime.Now.AddDays(19) } }
        },
        new Patron
        {
            PatronId = 5,
            Username = "Sam",
            FirstName = "Samuel",
            LastName = "Green",
            Email = "samuel.green@example.com",
            Password = "db0edd04aaac4506f7edab03ac855d56", // Presuming this is the hashed password
            PhoneNumber = "+1987654322",
            Address = "202 Birch Lane, Springfield, IL",
            MemberSince = new DateTime(2022, 7, 18),
            Role = "Patron",
            Loans = new List<Loan> { new Loan { LoanId = 5, BookId = 5, LoanDate = DateTime.Now.AddDays(-12), ReturnDate = DateTime.Now.AddDays(8) } }
        }
        };
        public static List<Librarian> Librarians = new List<Librarian>
        {
             // Seed
            new Librarian
        {
            LibrarianId = 1,
            Username = "admin1",
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "dde6a80ee27ed148c86020e2cfaf6d28" // Presuming this is the hashed password
        },
        new Librarian
        {
            LibrarianId = 2,
            Username = "Janee",
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane.smith@example.com",
            Password = "6cb75f652a9b52798eb6cf2201057c73" // Presuming this is the hashed password
        },
        new Librarian
        {
            LibrarianId = 3,
            Username = "Mikee",
            FirstName = "Mike",
            LastName = "Johnson",
            Email = "mike.johnson@example.com",
            Password = "819b0643d6b89dc9b579fdfc9094f28e" // Presuming this is the hashed password
        },
        new Librarian
        {
            LibrarianId = 4,
            Username = "Lisaa",
            FirstName = "Lisa",
            LastName = "Brown",
            Email = "lisa.brown@example.com",
            Password = "34cc93ece0ba9e3f6f235d4af979b16c" // Presuming this is the hashed password
        },
        new Librarian
        {
            LibrarianId = 5,
            Username = "Samm",
            FirstName = "Samuel",
            LastName = "Green",
            Email = "samuel.green@example.com",
            Password = "db0edd04aaac4506f7edab03ac855d56" // Presuming this is the hashed password
        }
        };
        public static List<Loan> Loans = new List<Loan>();
        public static List<BookCirculationReport> BookCirculationReports = new List<BookCirculationReport>();
        public static List<OverdueReport> OverdueReports = new List<OverdueReport>();
        public static List<PatronActivityReport> PatronActivityReports = new List<PatronActivityReport>();

        static InMemoryStorage()
        {
            // Seed Books
            Books.Add(new Book { BookId = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", ISBN = "9780743273565", Genre = "Fiction", TotalCopies = 10, AvailableCopies = 8, Status = "Available" });
            Books.Add(new Book { BookId = 2, Title = "1984", Author = "George Orwell", ISBN = "9780451524935", Genre = "Dystopian", TotalCopies = 15, AvailableCopies = 10, Status = "Available" });
            Books.Add(new Book { BookId = 3, Title = "To Kill a Mockingbird", Author = "Harper Lee", ISBN = "9780061120084", Genre = "Fiction", TotalCopies = 20, AvailableCopies = 18,  Status = "Available" });
            Books.Add(new Book { BookId = 4, Title = "The Catcher in the Rye", Author = "J.D. Salinger", ISBN = "9780316769488", Genre = "Fiction", TotalCopies = 8, AvailableCopies = 6, Status = "Available" });
            Books.Add(new Book { BookId = 5, Title = "Pride and Prejudice", Author = "Jane Austen", ISBN = "9780141439518", Genre = "Romance", TotalCopies = 12, AvailableCopies = 12, 
                Status = "Available" });

            // Seed Loans
            Loans.Add(new Loan { LoanId = 1, BookId = 1, PatronId = 2, LoanDate = DateTime.Now.AddDays(-5), DueDate = DateTime.Now.AddDays(5), FineAmount = 0, Status = "Active" });
            Loans.Add(new Loan { LoanId = 2, BookId = 3, PatronId = 3, LoanDate = DateTime.Now.AddDays(-10), DueDate = DateTime.Now.AddDays(2), FineAmount = 1.50m, Status = "Overdue" });
            Loans.Add(new Loan { LoanId = 3, BookId = 2, PatronId = 4, LoanDate = DateTime.Now.AddDays(-3), DueDate = DateTime.Now.AddDays(7), FineAmount = 0, Status = "Active" });
            Loans.Add(new Loan { LoanId = 4, BookId = 4, PatronId = 5, LoanDate = DateTime.Now.AddDays(-7), DueDate = DateTime.Now.AddDays(3), FineAmount = 0, Status = "Active" });

            // Seed Book Circulation Reports
            BookCirculationReports.Add(new BookCirculationReport { BookId = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", TotalLoans = 5 });
            BookCirculationReports.Add(new BookCirculationReport { BookId = 2, Title = "1984", Author = "George Orwell", TotalLoans = 10 });
            BookCirculationReports.Add(new BookCirculationReport { BookId = 3, Title = "To Kill a Mockingbird", Author = "Harper Lee", TotalLoans = 8 });

            // Seed Overdue Reports
            OverdueReports.Add(new OverdueReport { LoanId = 2, BookTitle = "To Kill a Mockingbird", PatronName = "Mike Jones", DueDate = DateTime.Now.AddDays(-2), FineAmount = 1.50m });

            // Seed Patron Activity Reports
            PatronActivityReports.Add(new PatronActivityReport { PatronName = "Jane Smith", TotalLoans = 12, OverdueLoans = 1 });
            PatronActivityReports.Add(new PatronActivityReport { PatronName = "Mike Jones", TotalLoans = 10, OverdueLoans = 2 });
        }
    }
}
