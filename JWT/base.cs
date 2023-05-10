using System;
using System.IO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

class Program
{
    static void Main(string[] args)
    {
        // Random secret key oluşturulması
        byte[] key = new byte[64];
        using (var generator = new RNGCryptoServiceProvider())
        {
            generator.GetBytes(key);
        }

        string base64Key = Convert.ToBase64String(key);

        // Secret key'in dosyaya kaydedilmesi
        File.WriteAllText("keys.txt", base64Key);

        // JWT token'in üretilmesi
        Console.Write("Enter issuer: ");
        string issuer = Console.ReadLine();

        Console.Write("Enter audience: ");
        string audience = Console.ReadLine();

        Console.Write("Enter expiration in minutes: ");
        int expiryInMinutes = Convert.ToInt32(Console.ReadLine());

        var securityKey = new SymmetricSecurityKey(key);
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
