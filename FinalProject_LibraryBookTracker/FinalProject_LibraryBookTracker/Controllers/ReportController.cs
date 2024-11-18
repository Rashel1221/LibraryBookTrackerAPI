using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinalProject_LibraryBookTracker.Model;
using static FinalProject_LibraryBookTracker.Model.Report;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Require authentication for all endpoints
public class ReportController : ControllerBase
{
    // Get book circulation report
    [HttpGet("GetBookCirculationReport")]
    public ActionResult<IEnumerable<BookCirculationReport>> GetBookCirculationReport()
    {
        // Simulate report generation (you would typically query the database here)
        var report = new List<BookCirculationReport>
        {
            new BookCirculationReport { BookId = 1, Title = "Book 1", Author = "Author 1", TotalLoans = 10 },
            new BookCirculationReport { BookId = 2, Title = "Book 2", Author = "Author 2", TotalLoans = 15 }
        };
        return Ok(report);
    }

    // Get overdue report
    [HttpGet("GetOverdueReport")]
    public ActionResult<IEnumerable<OverdueReport>> GetOverdueReport()
    {
        // Simulate overdue report
        var overdueReport = new List<OverdueReport>
        {
            new OverdueReport { LoanId = 1, BookTitle = "Book 1", PatronName = "Patron 1", DueDate = DateTime.Now.AddDays(-5), FineAmount = 5.0m },
            new OverdueReport { LoanId = 2, BookTitle = "Book 2", PatronName = "Patron 2", DueDate = DateTime.Now.AddDays(-3), FineAmount = 3.0m }
        };
        return Ok(overdueReport);
    }

    // Get patron activity report
    [HttpGet("GetPatronActivityReport")]
    public ActionResult<IEnumerable<PatronActivityReport>> GetPatronActivityReport()
    {
        // Simulate patron activity report
        var activityReport = new List<PatronActivityReport>
        {
            new PatronActivityReport { PatronName = "Patron 1", TotalLoans = 10, OverdueLoans = 2 },
            new PatronActivityReport { PatronName = "Patron 2", TotalLoans = 15, OverdueLoans = 3 }
        };
        return Ok(activityReport);
    }
}

