using Steam.Domain.Entities.Common;

namespace Steam.Domain.Entities.Catalog
{
    public class SystemRequirements : BaseEntity
    {
        public int ApplicationId { get; set; }

        public string RequirementType { get; set; } = default!;

        public string OS { get; set; } = default!;
        public string CPU { get; set; } = default!;
        public string GPU { get; set; } = default!;
        public string RAM { get; set; } = default!;
        public string Storage { get; set; } = default!;
        public string AdditionalNotes { get; set; } = default!;

        public ApplicationCatalog Application { get; set; } = default!;
    }
}
