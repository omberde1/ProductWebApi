using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductWebApi.Data.InMemory;
using ProductWebApi.DTOs.Auth;
using ProductWebApi.Models;
using ProductWebApi.Services;
using System.Security.Cryptography;
using System.Text;

namespace ProductWebApi.Controllers
{
    // #IMP# Primary Constructors in C#, dont need to create manual properties, instead now directly access the dependencies
    // If using interface, then Program.cs already passes the correct class to where the interface is being called
    [AllowAnonymous]
    [Route("api/auth")]
    [ApiController]
    public class AuthController(UserStore data, JwtService jwtService, RefreshTokenStore refreshTokenStore) : ControllerBase
    {
        // #IMP# [Route] simplified
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginRequest)
        {
            try
            {
                var user = data.Users.FirstOrDefault(u => u.Username == loginRequest.Username && u.Password == loginRequest.Password);
                if (user is null) return BadRequest("Invalid Username or Password. Try again.");

                var token = jwtService.GenerateToken(user);
                // #IMP# Generate the raw token to send to the client AND Compute SHA256 STRICTLY HASH to store in the Database
                string rawRefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
                byte[] hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawRefreshToken));
                string hashedRefreshToken = Convert.ToBase64String(hashBytes);
                // Debug.WriteLine($"{new string('*', 10)} #HashedRefreshToken-Login : {hashedRefreshToken} {new string('*', 10)}");

                var refreshTokenData = new RefreshToken
                {
                    UserId = user.Id,
                    HashedToken = hashedRefreshToken,
                    ExpiresAt = DateTime.UtcNow.AddDays(refreshTokenStore.ExpiryInDays),
                };
                refreshTokenStore.RefreshTokens.Add(refreshTokenData);

                var res = new TokenResponseDto
                {
                    BearerToken = token,
                    RefreshToken = rawRefreshToken
                };

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error : {ex}");
            }
        }
        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshDto refreshRequest)
        {
            try
            {
                // 1. Take the raw token sent by the client
                string clientRawToken = refreshRequest.RefreshToken;

                // 2. Hash it using the exact same algorithm
                byte[] clientHashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(clientRawToken));
                string clientTokenHash = Convert.ToBase64String(clientHashBytes);
                // #IMP# Debugging to Check if it always hashed and gives out the same string, but it give different each time
                // Debug.WriteLine($"{new string('*', 10)} #HashedRefreshToken-Refresh : {clientTokenHash} {new string('*', 10)}");

                // 3. Query your DB using the hash
                var tokenRecord = refreshTokenStore.RefreshTokens.FirstOrDefault(t => t.HashedToken == clientTokenHash);

                if (tokenRecord == null || tokenRecord.ExpiresAt < DateTime.UtcNow || tokenRecord.IsRevoked)
                    return Unauthorized("Invalid or expired refresh token.");

                // #IMP# Use find for Unique/PrimaryKey searching, Use FirstOD for non unique searching
                var user = data.Users.Find(u => u.Id == tokenRecord.UserId);
                if (user is null) return NotFound("Associted User not found.");

                var token = jwtService.GenerateToken(user);

                var res = new TokenResponseDto
                {
                    BearerToken = token,
                    RefreshToken = clientRawToken
                };

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error : {ex}");
            }
        }


    }
}
