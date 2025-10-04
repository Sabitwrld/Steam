namespace Steam.Application.DTOs.Library.License
{
    public record LicenseCreateDto
    {
        public int ApplicationId { get; init; }
        public string LicenseType { get; init; } = "Lifetime";
        public DateTime? ExpirationDate { get; init; }
    }
}
