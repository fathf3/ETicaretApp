using ETicaretServer.Application.Abstractions.Services;
using ETicaretServer.Application.Abstractions.Token;
using ETicaretServer.Application.DTOs;
using ETicaretServer.Application.Exceptions.AppUser;
using ETicaretServer.Application.Features.Commands.AppUser.LoginUser;
using ETicaretServer.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        IConfiguration _configuration;
        readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;

        public AuthService(ITokenHandler tokenHandler, UserManager<Domain.Entities.Identity.AppUser> userManager, IConfiguration configuration, SignInManager<AppUser> signInManager)
        {
            _tokenHandler = tokenHandler;
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        private async Task<Token> CreateUserExternalAsync(AppUser user, string email, string name, UserLoginInfo userInfo, int accessTokenLifeTime)
        {
            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = email,
                        UserName = email,
                        NameSurname = name,
                    };
                    var identityUser = await _userManager.CreateAsync(user);
                    result = identityUser.Succeeded;
                }
            }
            if (result)
            {
                await _userManager.AddLoginAsync(user, userInfo);
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime);
                return token;
            }
            throw new Exception("Invalid external authentication.");
        }


        public async Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["Google:Client_ID"] }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

            var info = new UserLoginInfo("Google", payload.Subject, "Google");
            Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

             return await CreateUserExternalAsync(user, payload.Email, payload.Name, info, accessTokenLifeTime);
            
            

        }

        public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
        {
            Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(usernameOrEmail);

            if (user == null)
                throw new NorFoundUserException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            
            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime);
                return token;

            }
           
            throw new AuthenticationErrorException();
        }
    }
}
