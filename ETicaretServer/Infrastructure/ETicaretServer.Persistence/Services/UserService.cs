using ETicaretServer.Application.Abstractions.Services;
using ETicaretServer.Application.DTOs.User;
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
    }
}
