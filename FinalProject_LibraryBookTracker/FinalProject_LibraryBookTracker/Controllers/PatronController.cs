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
public class PatronController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private const string Salt = ")GN#447#^nryrETNwrbR%#&NBRE%#%BBDT#%"; // Define the salt here

    public PatronController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("Login")]
    public ActionResult Login([FromBody] LoginDto loginDto)
    {
        // Hash the incoming password with the salt
        string hashedPassword = HashPassword(loginDto.Password);

        // Check if the hashed password matches a patron in the list
        var patron = InMemoryStorage.Patrons.FirstOrDefault(p => p.Username == loginDto.Username && p.Password == hashedPassword);
        if (patron == null)
        {
            return Unauthorized("Invalid login credentials.");
        }

        var token = GenerateJwtToken(patron, loginDto.Platform);
        return Ok(new { Token = token, Message = "Login successful." });
    }

    [HttpPost("Logout")]
    public ActionResult Logout()
    {
        // Note: Logout is typically handled client-side by removing the JWT token.
        return Ok("Logout successful.");
    }

    [HttpPost("Register")]
    public ActionResult Register([FromBody] Patron newPatron)
    {
        var existingPatron = InMemoryStorage.Patrons.FirstOrDefault(p => p.Email == newPatron.Email);
        if (existingPatron != null)
        {
            return BadRequest("Email is already registered. Please choose a different one.");
        }

        // Hash the password before storing it
        newPatron.Password = HashPassword(newPatron.Password);

        newPatron.PatronId = InMemoryStorage.Patrons.Count + 1;
        InMemoryStorage.Patrons.Add(newPatron);
        return Ok("Registered successfully.");
    }

    [HttpPut("UpdateProfile/{id}")]
    public ActionResult UpdateProfile(int id, [FromBody] Patron updatedPatron)
    {
        var patron = InMemoryStorage.Patrons.FirstOrDefault(p => p.PatronId == id);
        if (patron == null) return NotFound("Patron not found.");

        patron.Email = updatedPatron.Email;

        // Update and hash the password if provided
        if (!string.IsNullOrEmpty(updatedPatron.Password))
        {
            patron.Password = HashPassword(updatedPatron.Password);
        }

        return Ok("Profile updated successfully.");
    }

    [HttpGet("GetAllPatrons")]
    public ActionResult<IEnumerable<Patron>> GetAllPatrons()
    {
        var patrons = InMemoryStorage.Patrons;
        if (patrons == null || patrons.Count == 0)
        {
            return NotFound("No patrons found.");
        }

        return Ok(patrons);
    }

    [HttpGet("GetPatron/{id}")]
    public ActionResult<Patron> GetPatron(int id)
    {
        var patron = InMemoryStorage.Patrons.FirstOrDefault(p => p.PatronId == id);
        if (patron == null)
        {
            return NotFound("Patron not found.");
        }

        return Ok(patron);
    }

    private string GenerateJwtToken(Patron patron, string platform)
    {
        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, patron.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("PatronId", patron.PatronId.ToString()),
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
