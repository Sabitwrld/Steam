namespace Steam.Application.DTOs.Catalog.SystemRequirements
{
    public record SystemRequirementsListItemDto
    {
        public int Id { get; init; }
        public string RequirementType { get; init; } = default!;
        public string OS { get; init; } = default!;
        public string CPU { get; init; } = default!;
        public string GPU { get; init; } = default!;
        public string RAM { get; init; } = default!;
        public string Storage { get; init; } = default!;
    }
}
