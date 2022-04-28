//HTTPPOST: https://localhost:7033/api/authentication/authenticate
// Body:raw + JSON -> { "user" : "Mateusz", "password": "SecretPassword" } 


namespace WebAPI_ASP.NET6.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public class AuthenticationBody
        {
            public string? UserName { get; set; }
            public string? Password { get; set; }
        }
        private class ProductInfoUser
        {
            public int UserId { get; set; }
            public string MailAddress { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public ProductInfoUser(int userId, string mailAddress, string firstName, string lastName)
            {
                UserId = userId;
                MailAddress = mailAddress;
                FirstName = firstName;
                LastName = lastName;
            }
        }

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationBody authenticationBody)
        {
            var user = CreateCredentials(
                authenticationBody.UserName, authenticationBody.Password);

            if (user == null)
            {
                return Unauthorized();
            }
            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));

            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.UserId.ToString()));
            claimsForToken.Add(new Claim("mail", user.MailAddress));
            claimsForToken.Add(new Claim("given_name", user.FirstName));
            claimsForToken.Add(new Claim("family_name", user.LastName));

            var securityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(5),
                signingCredentials);
             
            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return Ok(tokenToReturn);
        }

        private ProductInfoUser CreateCredentials(string? userName, string? password)
        {
            return new ProductInfoUser(1, "developer@example.com", "Mateusz", "Gabała");
        }
    }
}
