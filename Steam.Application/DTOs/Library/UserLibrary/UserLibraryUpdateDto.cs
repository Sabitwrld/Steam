namespace Steam.Application.DTOs.Library.UserLibrary
{
    public record UserLibraryUpdateDto
    {
        // For now, there isn't much to update on a library itself, 
        // but we keep the DTO for future features. 
        // We'll use the Id from the route.
        public string UserId { get; init; }
    }
}
