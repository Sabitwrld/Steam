namespace Steam.Application.DTOs.Library.License
{
    public record LicenseListItemDto
    {
        public int Id { get; init; }
        public int ApplicationId { get; init; }
        public string ApplicationName { get; init; } = default!;
        public int PlaytimeInMinutes { get; init; }
        public DateTime? LastPlayed { get; init; }
    }
}
