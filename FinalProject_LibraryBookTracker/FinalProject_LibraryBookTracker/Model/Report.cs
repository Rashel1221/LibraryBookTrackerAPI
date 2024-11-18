using System;

namespace FinalProject_LibraryBookTracker.Model
{
    public class Report
    {
public class BookCirculationReport
        {
            public int BookId { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public int TotalLoans { get; set; }
        }

public class OverdueReport
        {
            public int LoanId { get; set; }
            public string BookTitle { get; set; }
            public string PatronName { get; set; }
            public DateTime DueDate { get; set; }
            public decimal FineAmount { get; set; }
        }

public class PatronActivityReport
        {
            public string PatronName { get; set; }
            public int TotalLoans { get; set; }
            public int OverdueLoans { get; set; }
        }
    }
}
