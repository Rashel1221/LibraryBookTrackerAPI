using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinalProject_LibraryBookTracker.Model;
using FinalProject_LibraryBookTracker.DataHelper;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Require authentication for all endpoints
public class LoanController : ControllerBase
{
    // Get all loans
    [HttpGet("GetAllLoans")]
    public ActionResult<IEnumerable<Loan>> GetAllLoans()
    {
        return Ok(InMemoryStorage.Loans);
    }

    // Add a new loan
    [HttpPost("AddLoan")]
    public ActionResult AddLoan([FromBody] Loan loan)
    {
        if (loan == null || loan.BookId <= 0 || loan.UserId <= 0 || loan.DueDate < loan.LoanDate)
        {
            return BadRequest("Invalid loan data.");
        }

        loan.LoanId = InMemoryStorage.Loans.Count + 1;
        InMemoryStorage.Loans.Add(loan);
        return Ok("Loan added successfully.");
    }

    // Update an existing loan
    [HttpPut("UpdateLoan/{id}")]
    public ActionResult UpdateLoan(int id, [FromBody] Loan updatedLoan)
    {
        var loan = InMemoryStorage.Loans.FirstOrDefault(l => l.LoanId == id);
        if (loan == null) return NotFound("Loan not found.");

        loan.BookId = updatedLoan.BookId;
        loan.UserId = updatedLoan.UserId;
        loan.LoanDate = updatedLoan.LoanDate;
        loan.DueDate = updatedLoan.DueDate;
        loan.ReturnDate = updatedLoan.ReturnDate;
        loan.FineAmount = updatedLoan.FineAmount;
        loan.Status = updatedLoan.Status;

        return Ok("Loan updated successfully.");
    }

    // Delete a loan
    [HttpDelete("DeleteLoan/{id}")]
    public ActionResult DeleteLoan(int id)
    {
        var loan = InMemoryStorage.Loans.FirstOrDefault(l => l.LoanId == id);
        if (loan == null) return NotFound("Loan not found.");

        InMemoryStorage.Loans.Remove(loan);
        return Ok("Loan deleted successfully.");
    }

    // Get loan by ID
    [HttpGet("GetLoan/{id}")]
    public ActionResult<Loan> GetLoan(int id)
    {
        var loan = InMemoryStorage.Loans.FirstOrDefault(l => l.LoanId == id);
        if (loan == null) return NotFound("Loan not found.");

        return Ok(loan);
    }
}

