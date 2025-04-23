namespace Gym_Community.API.DTOs.Auth
{
    public class ExternalLoginDTO
    {
        public string Provider { get; set; } // "Google" or "Facebook"
        public string IdToken { get; set; } // Token from the external provider
    }
}
