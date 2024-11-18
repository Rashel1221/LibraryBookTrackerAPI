namespace FinalProject_LibraryBookTracker.Model
{
    public class Loan
    {
        public int LoanId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public Book Book { get; set; }  // Navigation property to Book

        public int PatronId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }  // Nullable to allow for non-returned books
        public decimal FineAmount { get; set; }
        public string Status { get; set; }  // e.g., Active, Overdue, Returned
    }

}
