using Steam.Application.DTOs.Library.License;

namespace Steam.Application.Services.Library.Interfaces
{
    public interface ILicenseService
    {
        Task<LicenseReturnDto> AddLicenseAsync(int userLibraryId, LicenseCreateDto dto);
        Task<LicenseReturnDto> UpdateLicenseAsync(int id, LicenseUpdateDto dto);
        Task<bool> DeleteLicenseAsync(int id); // For admin or special cases
        Task<LicenseReturnDto> GetLicenseByIdAsync(int id);
    }
}
