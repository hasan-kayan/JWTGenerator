using System;
using System.Collections.Generic;
using System.IO;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

class Program
{
    static void Main(string[] args)
    {
        // Random secret key creation 
        byte[] key = new byte[64];
        using (var generator = new RNGCryptoServiceProvider())
        {
            generator.GetBytes(key);
        }

        // TypeCasting Part to string 
        string base64Key = Convert.ToBase64String(key);


        // Writing the secret key in case of if we want to change the parameters later in precaution
        File.WriteAllText("keys.txt", base64Key);


        // User input for token data 
        var payload = new Dictionary<string, object>();
        while (true)
        {
            Console.WriteLine("Enter the key for the claim (or type 'exit' to finish):"); // Take the parameter header
            string keyName = Console.ReadLine();

            if (keyName == "exit")
            {
                break;
            }

            Console.WriteLine("Enter the value for the claim:"); // Take the parameter value
            string keyValue = Console.ReadLine();

            payload.Add(keyName, keyValue);
        }

        // SymmetricSecurityKey and SigningCredentials objects 
        // This objects will be used while the signutre part of JWT creating 
        var securityKey = new SymmetricSecurityKey(key);
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);


        // Payload data is importing to JWT 
        var claims = payload.Select(x => new Claim(x.Key, x.Value.ToString())).ToArray();

        // JwtSecurityToken Object Creation 
        var token = new JwtSecurityToken(
            expires: DateTime.UtcNow.AddHours(1), // Expires Time Valued
            claims: claims,
            signingCredentials: signingCredentials
        );

        // JwtSecurityToken Object TypeCasting to String 
        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
        Console.WriteLine("Generated JWT token: " + jwtToken);

        Console.ReadLine();
    }
}
