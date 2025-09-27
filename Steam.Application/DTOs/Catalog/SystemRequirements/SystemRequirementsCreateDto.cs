namespace Steam.Application.DTOs.Catalog.SystemRequirements
{
    public record SystemRequirementsCreateDto
    {
        public int ApplicationId { get; init; }
        public string RequirementType { get; init; } = default!;  // Minimum / Recommended
        public string OS { get; init; } = default!;
        public string CPU { get; init; } = default!;
        public string GPU { get; init; } = default!;
        public string RAM { get; init; } = default!;
        public string Storage { get; init; } = default!;
        public string AdditionalNotes { get; init; } = default!;
    }
}
