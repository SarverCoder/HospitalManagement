namespace HospitalManagement.JwtConfiguration.Services
{
    public interface IAuthService
    {
        string GetToken(string username);
    }
}
