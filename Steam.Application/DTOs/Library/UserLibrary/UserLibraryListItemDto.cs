    namespace Steam.Application.DTOs.Library.UserLibrary
{
    public record UserLibraryListItemDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public int LicenseCount { get; init; }
    }
}
