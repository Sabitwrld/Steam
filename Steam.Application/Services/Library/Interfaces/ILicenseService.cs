using Steam.Application.DTOs.Library.License;
using Steam.Application.DTOs.Pagination;

namespace Steam.Application.Services.Library.Interfaces
{
    public interface ILicenseService
    {
        Task<LicenseReturnDto> CreateLicenseAsync(int libraryId, LicenseCreateDto dto);
        Task<LicenseReturnDto> UpdateLicenseAsync(int id, LicenseUpdateDto dto);
        Task<bool> DeleteLicenseAsync(int id);
        Task<LicenseReturnDto> GetLicenseByIdAsync(int id);
        Task<PagedResponse<LicenseListItemDto>> GetAllLicensesAsync(int pageNumber, int pageSize);
    }
}
