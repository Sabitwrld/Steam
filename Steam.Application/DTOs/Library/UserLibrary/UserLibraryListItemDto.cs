namespace Steam.Application.DTOs.Library.UserLibrary
{
    public record UserLibraryListItemDto
    {
        public int Id { get; init; }
        public string UserId { get; init; }
        public int LicenseCount { get; init; }
    }
}
