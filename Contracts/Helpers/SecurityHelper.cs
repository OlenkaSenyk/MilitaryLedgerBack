using Domain.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Helpers
{
    public class SecurityHelper
    {
        public static int GetClaimsFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken != null)
            {
                var userIdClaim = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "unique_name");

                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    return userId;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                throw new ArgumentException("Invalid token", nameof(token));
            }
        }

        public static string GetHashedString(string text)
        {
            return BCrypt.Net.BCrypt.HashPassword(text);
        }

        public static string CreateToken(string secretKey, string id, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, id),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static bool VerifyHash(string text, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(text, hash);
        }

        public static int DecryptInteger(string ciphertext, Func<string, string> protector)
        {
            return int.Parse(protector(ciphertext));
        }

        public static DateOnly DecryptDate(string ciphertext, Func<string, string> protector)
        {
            return DateOnly.Parse(protector(ciphertext));
        }

        public static bool DecryptBool(string ciphertext, Func<string, string> protector)
        {
            return bool.Parse(protector(ciphertext));
        }

        public static void ProtectFields(object source, object destination, string[] fields, Func<string, string> protector)
        {
            foreach (var field in fields)
            {
                var sourceValue = source.GetType().GetProperty(field)?.GetValue(source)?.ToString();
                var destinationProperty = destination.GetType().GetProperty(field);
                if (destinationProperty != null)
                {
                    var protectedValue = protector(sourceValue);
                    destinationProperty.SetValue(destination, protectedValue);
                }
            }
        }

        public static void UnprotectFields(object source, object destination, string[] fields, Func<string, string> protector)
        {
            foreach (var field in fields)
            {
                try
                {
                    var sourceValue = source.GetType().GetProperty(field)?.GetValue(source)?.ToString();
                    var destinationProperty = destination.GetType().GetProperty(field);
                    var fieldType = destinationProperty.PropertyType;

                    object protectedValue = null;

                    if (sourceValue != null)
                    {
                        if (fieldType == typeof(int?))
                            protectedValue = DecryptInteger(sourceValue, protector);
                        else if (fieldType == typeof(DateOnly))
                            protectedValue = DecryptDate(sourceValue, protector);
                        else if (fieldType == typeof(bool))
                            protectedValue = DecryptBool(sourceValue, protector);
                        else
                            protectedValue = protector(sourceValue);
                    }

                    destinationProperty.SetValue(destination, protectedValue);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error decrypting string: {ex.Message}");
                }
            }
        }

        public static string[] GetAllFieldsNames(object source)
        {
            var sourceType = source.GetType();
            var properties = sourceType.GetProperties();
            var fieldNames = properties.Select(property => property.Name).ToArray();
            return fieldNames;
        }
    }
}
