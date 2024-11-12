namespace ETicaretServer.Application.Abstractions.Services.Authentication
{
    public interface IExternalAuthentication
    {
        Task<DTOs.Token>GoogleLoginAsync(string idToken, int accessTokenLifeTime);
        Task<DTOs.Token> RefreshTokenLoginAsync(string refreshToken);
        //Task FacebookLoginAsync();
        //Task TwitterLoginAsync();
    }
}
