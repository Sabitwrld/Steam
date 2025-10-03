using Steam.Application.DTOs.Library.License;

namespace Steam.Application.DTOs.Library.UserLibrary
{
    public record UserLibraryReturnDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public List<LicenseReturnDto> Licenses { get; init; } = new();
    }
}
