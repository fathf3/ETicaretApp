﻿using ETicaretServer.Application.Abstractions.Token;
using ETicaretServer.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Application.DTOs.Token CreateAccessToken(int second, AppUser user, IList<string> userRole)
        {
            Application.DTOs.Token token = new();

            // Security Key'in simetrigini aliyoruz
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));


            // Sifrelenmis kimligi olusturuyoruz.
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            // olusturulacak token ayarlarıni belirliyoruz.

            token.Expiration = DateTime.UtcNow.AddSeconds(second);
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName), // Kullanıcı ID'si
        
    };
            claims.AddRange(userRole.Select(role => new Claim(ClaimTypes.Role, role)));
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,

                claims: claims

                );

            // Token olusturucu sinifindan ornek alalim
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);

            token.RefreshToken = CreateRefreshToken();

            return token;

        }

        public string CreateRefreshToken()
        {
            // Rastgele refresh token olusturuyoruz
            byte[] number = new byte[32];
            // using metod gorevini tamamladıkdan sonra despose edilir.
            using RandomNumberGenerator random = RandomNumberGenerator.Create();

            random.GetBytes(number);
            return Convert.ToBase64String(number);

        }
    }
}
