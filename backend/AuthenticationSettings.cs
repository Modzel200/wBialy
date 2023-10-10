namespace wBialy
{
    public class AuthenticationSettings
    {
#pragma warning disable CS8618
        public string JwtKey { get; set; }
        public int JwtExpireDays { get; set; }
        public string JwtIssuer { get; set; }
#pragma warning restore CS8618
    }
}
