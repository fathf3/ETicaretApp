using ETicaretServer.Application.Abstractions.Services;
using ETicaretServer.Application.DTOs.User;
using ETicaretServer.Application.Exceptions.AppUser;
using ETicaretServer.Application.Helpers;
using ETicaretServer.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ETicaretServer.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                NameSurname = model.NameSurname,
                UserName = model.Username,
                Email = model.Email,

            }, model.Password); ;

            CreateUserResponse response = new() { Succeed = result.Succeeded };


            if (result.Succeeded)
                response.Message = $"{model.Username} kullanıcısı oluşturuldu.";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}";

            return response;
        }

        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();
                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                }
                else
                    throw new PasswordChangeFailedException();


            }
        }

        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int refreshTokenLifeTime)
        {

            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(refreshTokenLifeTime);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new Exception("Kullanıcı bulunamadı");
        }
    }
}
