namespace Steam.Domain.Settings
{
    public class AuthSettings
    {
        public string JwtKey { get; set; } = null!;
        public string JwtIssuer { get; set; } = null!;
        public string JwtAudience { get; set; } = null!;
        public int JwtExpireHours { get; set; } = 1;

        public string SmtpHost { get; set; } = null!;
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; } = null!;
        public string SmtpPass { get; set; } = null!;
        public bool SmtpUseSSL { get; set; } = true;
    }
}
