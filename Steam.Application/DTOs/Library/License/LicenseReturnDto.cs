namespace Steam.Application.DTOs.Library.License
{
    public record LicenseReturnDto
    {
        public int Id { get; init; }
        public int ApplicationId { get; init; }
        public string ApplicationName { get; init; } = default!; // For display
        public string LicenseType { get; init; } = default!;
        public DateTime? ExpirationDate { get; init; }
        public int PlaytimeInMinutes { get; init; }
        public DateTime? LastPlayed { get; init; }
        public bool IsHidden { get; init; }
    }
}
