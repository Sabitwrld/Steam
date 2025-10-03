namespace Steam.Application.DTOs.Library.License
{
    public record LicenseReturnDto
    {
        public int Id { get; init; }
        public int ApplicationId { get; init; }
        public string LicenseType { get; init; } = default!;
        public DateTime? ExpirationDate { get; init; }
    }
}
