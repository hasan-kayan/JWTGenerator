using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

class Program
{
    static void Main(string[] args)
    {
        // Kullanıcıdan gerekli bilgilerin alınması
        Console.Write("Enter secret key: ");
        string secretKey = Console.ReadLine();

        Console.Write("Enter issuer: ");
        string issuer = Console.ReadLine();

        Console.Write("Enter audience: ");
        string audience = Console.ReadLine();

        Console.Write("Enter expiration in minutes: ");
        int expiryInMinutes = Convert.ToInt32(Console.ReadLine());

        // JWT token'in üretilmesi
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, "Hasan Kayan"),
            new Claim(ClaimTypes.Email, "hasankayan2000@hotmail.com")
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
            signingCredentials: signingCredentials
        );

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
        Console.WriteLine("Generated JWT token: " + jwtToken);

        Console.ReadLine();
    }
}
