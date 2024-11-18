using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinalProject_LibraryBookTracker.Model;
using FinalProject_LibraryBookTracker.DataHelper;

[ApiController]
[Route("api/[controller]")]
//[Authorize] // Require authentication for all endpoints
public class BookController : ControllerBase
{
    // Get all books
    [HttpGet("GetAllBooks")]
    public ActionResult<IEnumerable<Book>> GetAllBooks()
    {
        return Ok(InMemoryStorage.Books);
    }

    // Add a new book
    [HttpPost("AddBook")]
    public ActionResult AddBook([FromBody] Book book)
    {
        if (book == null || string.IsNullOrEmpty(book.Title) || book.TotalCopies <= 0)
        {
            return BadRequest("Invalid book data.");
        }

        book.BookId = InMemoryStorage.Books.Count + 1;
        InMemoryStorage.Books.Add(book);
        return Ok("Book added successfully.");
    }

    // Update an existing book
    [HttpPut("UpdateBook/{id}")]
    public ActionResult UpdateBook(int id, [FromBody] Book updatedBook)
    {
        var book = InMemoryStorage.Books.FirstOrDefault(b => b.BookId == id);
        if (book == null) return NotFound("Book not found.");

        book.Title = updatedBook.Title;
        book.Author = updatedBook.Author;
        book.ISBN = updatedBook.ISBN;
        book.Genre = updatedBook.Genre;
        book.TotalCopies = updatedBook.TotalCopies;
        book.AvailableCopies = updatedBook.AvailableCopies;
        book.Status = updatedBook.Status;

        return Ok("Book updated successfully.");
    }

    // Delete a book
    [HttpDelete("DeleteBook/{id}")]
    public ActionResult DeleteBook(int id)
    {
        var book = InMemoryStorage.Books.FirstOrDefault(b => b.BookId == id);
        if (book == null) return NotFound("Book not found.");

        InMemoryStorage.Books.Remove(book);
        return Ok("Book deleted successfully.");
    }

    // Get book by ID
    [HttpGet("GetBook/{id}")]
    public ActionResult<Book> GetBook(int id)
    {
        var book = InMemoryStorage.Books.FirstOrDefault(b => b.BookId == id);
        if (book == null) return NotFound("Book not found.");

        return Ok(book);
    }
}

