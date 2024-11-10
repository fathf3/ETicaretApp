namespace ETicaretServer.Application.Abstractions.Services.Authentication
{
    public interface IExternalAuthentication
    {
        Task<DTOs.Token>GoogleLoginAsync(string idToken, int accessTokenLifeTime);
        //Task FacebookLoginAsync();
        //Task TwitterLoginAsync();
    }
}
