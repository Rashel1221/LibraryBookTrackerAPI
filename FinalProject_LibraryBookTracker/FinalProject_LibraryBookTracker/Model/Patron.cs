namespace FinalProject_LibraryBookTracker.Model
{
    public class Patron
    {
        public int PatronId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime MemberSince { get; set; }

        public ICollection<Loan> Loans { get; set; }  // Navigation property for loans
        public string Role { get; set; }  // Role reference (e.g., Patron)
    }
}
