using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Catalog.Media;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Catalog.Interfaces;
using Steam.Domain.Entities.Catalog;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Catalog.Implementations
{
    public class MediaService : IMediaService
    {
        private readonly IRepository<Media> _repo;
        private readonly IMapper _mapper;
        private readonly FileService _fileService;

        public MediaService(IRepository<Media> repo, IMapper mapper, FileService fileService)
        {
            _repo = repo;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<MediaReturnDto> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id, q => q.Include(m => m.Application));
            return _mapper.Map<MediaReturnDto>(entity);
        }

        public async Task<PagedResponse<MediaListItemDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            var entities = await _repo.GetAllAsync(skip: (pageNumber - 1) * pageSize, take: pageSize);
            return new PagedResponse<MediaListItemDto>
            {
                Data = _mapper.Map<List<MediaListItemDto>>(entities),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = await _repo.GetQuery().CountAsync()
            };
        }

        // We need a new method to handle file uploads
        public async Task<MediaReturnDto> CreateWithFileAsync(MediaUploadDto dto)
        {
            // 1. Save the file and get its URL
            var fileUrl = await _fileService.SaveFileAsync(dto.File);

            // 2. Create the entity
            var entity = new Media
            {
                ApplicationId = dto.ApplicationId,
                MediaType = dto.MediaType,
                Order = dto.Order,
                Url = fileUrl // Use the URL from the saved file
            };

            // 3. Save to database
            await _repo.CreateAsync(entity);
            return _mapper.Map<MediaReturnDto>(entity);
        }

        // This method can remain for creating media with an existing URL
        public async Task<MediaReturnDto> CreateAsync(MediaCreateDto dto)
        {
            var entity = _mapper.Map<Media>(dto);
            await _repo.CreateAsync(entity);
            return _mapper.Map<MediaReturnDto>(entity);
        }

        // Update the DeleteAsync method to also delete the file
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            // Delete from database (soft delete)
            var result = await _repo.DeleteAsync(entity);

            if (result)
            {
                // Delete the physical file
                _fileService.DeleteFile(entity.Url);
            }

            return result;
        }

        public async Task<MediaReturnDto> UpdateAsync(int id, MediaUpdateDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new Exception("Media not found");

            _mapper.Map(dto, entity);
            await _repo.UpdateAsync(entity);
            return _mapper.Map<MediaReturnDto>(entity);
        }


    }
}
