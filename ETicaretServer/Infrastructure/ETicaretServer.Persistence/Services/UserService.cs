using ETicaretServer.Application.Abstractions.Services;
using ETicaretServer.Application.DTOs.User;
using ETicaretServer.Application.Exceptions.AppUser;
using ETicaretServer.Application.Helpers;
using ETicaretServer.Application.Repositories;
using ETicaretServer.Domain.Entities;
using ETicaretServer.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETicaretServer.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        readonly IEndpointReadRepository _endpointReadRepository;

        public UserService(UserManager<AppUser> userManager, IEndpointReadRepository endpointReadRepository)
        {
            _userManager = userManager;
            _endpointReadRepository = endpointReadRepository;
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

        public async Task<List<ListUser>> GetAllUsersAsync(int page, int size)
        {
            var users = await _userManager.Users
                .Skip(page * size)
                .Take(size)
                .ToListAsync();
            return users.Select(user => new ListUser
            {
                Id = user.Id,
                Email = user.Email,
                TwoFactorEnabled = user.TwoFactorEnabled,
                NameSurname = user.NameSurname,
                Username = user.UserName
            }).ToList();
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

        public int TotalUser() => _userManager.Users.Count();

        public async Task AssignRoleToUser(string userId, string[] roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);
                await _userManager.AddToRolesAsync(user, roles);
            }

        }
        public async Task<string[]> GetRolesToUserAsync(string userIdOrName)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userIdOrName);
            if (appUser == null)
            {
                appUser = await _userManager.FindByNameAsync(userIdOrName);
            }
            if (appUser != null)
            {
                var userRoles = await _userManager.GetRolesAsync(appUser);
                return userRoles.ToArray();
            }
            return new string[] { };

        }

        public async Task<bool> HasRolePermissionToEndpointAsync(string userName, string code)
        {
            var userRoles = await GetRolesToUserAsync(userName);
            if (!userRoles.Any())
                return false;

            EndPoint? endPoint = await _endpointReadRepository.Table
                .Include(e => e.AppRoles)
                .FirstOrDefaultAsync(e => e.Code == code);

            if (endPoint == null)
                return false;

            
            var endPointRoles = endPoint.AppRoles.Select(r => r.Name);
            foreach ( var userRole in userRoles)
            {
                foreach (var endPointRole in endPointRoles)
                    if (userRole == endPointRole)
                        return true;
            }
            return false;

        }
    }
}
