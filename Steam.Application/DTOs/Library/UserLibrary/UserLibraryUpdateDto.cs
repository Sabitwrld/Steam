namespace Steam.Application.DTOs.Library.UserLibrary
{
    public record UserLibraryUpdateDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
    }
}
