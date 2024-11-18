using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FinalProject_LibraryBookTracker.DataHelper;
using FinalProject_LibraryBookTracker.Model.DTO;
using FinalProject_LibraryBookTracker.Model;

[ApiController]
[Route("api/[controller]")]
public class LibrarianController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private const string Salt = ")GN#447#^nryrETNwrbR%#&NBRE%#%BBDT#%"; // Define the salt here

    public LibrarianController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("Login")]
    public ActionResult Login([FromBody] LoginDto loginDto)
    {
        // Hash the incoming password with the salt
        string hashedPassword = HashPassword(loginDto.Password);

        // Check if the hashed password matches a librarian in the list
        var librarian = InMemoryStorage.Librarians.FirstOrDefault(l => l.Username == loginDto.Username && l.Password == hashedPassword);
        if (librarian == null)
        {
            return Unauthorized("Invalid login credentials.");
        }

        var token = GenerateJwtToken(librarian, loginDto.Platform);
        return Ok(new { Token = token, Message = "Login successful." });
    }

    [HttpPost("Logout")]
    public ActionResult Logout()
    {
        // Note: Logout is typically handled client-side by removing the JWT token.
        return Ok("Logout successful.");
    }

    [HttpPost("Register")]
    public ActionResult Register([FromBody] Librarian newLibrarian)
    {
        var existingLibrarian = InMemoryStorage.Librarians.FirstOrDefault(l => l.Email == newLibrarian.Email);
        if (existingLibrarian != null)
        {
            return BadRequest("Email is already registered. Please choose a different one.");
        }

        // Hash the password before storing it
        newLibrarian.Password = HashPassword(newLibrarian.Password);

        newLibrarian.LibrarianId = InMemoryStorage.Librarians.Count + 1;
        InMemoryStorage.Librarians.Add(newLibrarian);
        return Ok("Registered successfully.");
    }

    [HttpPut("UpdateProfile/{id}")]
    public ActionResult UpdateProfile(int id, [FromBody] Librarian updatedLibrarian)
    {
        var librarian = InMemoryStorage.Librarians.FirstOrDefault(l => l.LibrarianId == id);
        if (librarian == null) return NotFound("Librarian not found.");

        librarian.Email = updatedLibrarian.Email;

        // Update and hash the password if provided
        if (!string.IsNullOrEmpty(updatedLibrarian.Password))
        {
            librarian.Password = HashPassword(updatedLibrarian.Password);
        }

        return Ok("Profile updated successfully.");
    }

    [HttpGet("GetAllLibrarians")]
    public ActionResult<IEnumerable<Librarian>> GetAllLibrarians()
    {
        var librarians = InMemoryStorage.Librarians;
        if (librarians == null || librarians.Count == 0)
        {
            return NotFound("No librarians found.");
        }

        return Ok(librarians);
    }

    [HttpGet("GetLibrarian/{id}")]
    public ActionResult<Librarian> GetLibrarian(int id)
    {
        var librarian = InMemoryStorage.Librarians.FirstOrDefault(l => l.LibrarianId == id);
        if (librarian == null)
        {
            return NotFound("Librarian not found.");
        }

        return Ok(librarian);
    }

    private string GenerateJwtToken(Librarian librarian, string platform)
    {
        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, librarian.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("LibrarianId", librarian.LibrarianId.ToString()),
            new Claim("Platform", platform)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwSettings:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _configuration["JwSettings:Issuer"],
            _configuration["JwSettings:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string HashPassword(string password)
    {
        using (var md5 = MD5.Create())
        {
            var saltedPassword = Salt + password;
            var hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}
