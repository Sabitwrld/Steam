namespace Steam.Application.DTOs.Library.License
{
    /// <summary>
    /// DTO for user actions like hiding a game or for the system to update playtime.
    /// </summary>
    public record LicenseUpdateDto
    {
        public int? PlaytimeInMinutes { get; init; }
        public DateTime? LastPlayed { get; init; }
        public bool? IsHidden { get; init; }
    }
}
