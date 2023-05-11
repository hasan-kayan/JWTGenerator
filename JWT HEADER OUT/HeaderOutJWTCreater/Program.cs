// dotnet add package System.IdentityModel.Tokens.Jwt --version 6.30.0
// dotnet add package Newtonsoft.Json --version 13.0.3

using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System;

namespace CSharp_Tests;

class Program2
{
    static void Main(string[] args)
    {
        // Create a new SymmetricSecurityKey using the specified key as a UTF-8 encoded string.
        var securityKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes("1234567890-1234567890_HASk352^++%&&dsnvlkbnkl1234567890_HASk352^++%&&dsnvlkbnkl")
        );
        // Create a new SigningCredentials with the specified key and algorithm.
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

        // Create a new JwtHeader with the specified SigningCredentials.
        var header = new JwtHeader(credentials);

        // header.Remove("typ");

        // Read the payload from a text file in json format
        var payload = JsonConvert.DeserializeObject<JwtPayload>(
            File.ReadAllText("json_payload.txt")
        );

        // Create a new JwtSecurityToken with the specified header and payload.
        var secToken = new JwtSecurityToken(header, payload);
        // Create a new JwtSecurityTokenHandler.
        var handler = new JwtSecurityTokenHandler();

        // Serialize the specified JwtSecurityToken to a string.
        var tokenString = handler.WriteToken(secToken);
        Console.WriteLine(tokenString);
    }
}
