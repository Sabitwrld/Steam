using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Catalog.Media;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Exceptions;
using Steam.Application.Services.Catalog.Interfaces;
using Steam.Domain.Entities.Catalog;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Catalog.Implementations
{
    public class MediaService : IMediaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public MediaService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<MediaReturnDto> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.MediaRepository.GetByIdAsync(id, q => q.Include(m => m.Application));
            if (entity == null) throw new NotFoundException(nameof(Media), id);
            return _mapper.Map<MediaReturnDto>(entity);
        }

        public async Task<PagedResponse<MediaListItemDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            var (items, totalCount) = await _unitOfWork.MediaRepository.GetAllPagedAsync(pageNumber, pageSize);

            return new PagedResponse<MediaListItemDto>
            {
                Data = _mapper.Map<List<MediaListItemDto>>(items),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<MediaReturnDto> CreateWithFileAsync(MediaUploadDto dto)
        {
            var fileUrl = await _fileService.SaveFileAsync(dto.File);
            var entity = new Media
            {
                ApplicationId = dto.ApplicationId,
                MediaType = dto.MediaType,
                Order = dto.Order,
                Url = fileUrl
            };

            await _unitOfWork.MediaRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<MediaReturnDto>(entity);
        }

        public async Task<MediaReturnDto> CreateAsync(MediaCreateDto dto)
        {
            var entity = _mapper.Map<Media>(dto);
            await _unitOfWork.MediaRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<MediaReturnDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.MediaRepository.GetByIdAsync(id);
            if (entity == null) return false;

            // Əvvəlcə faylı buluddan silirik
            await _fileService.DeleteFile(entity.Url); // DƏYİŞDİRİLDİ

            _unitOfWork.MediaRepository.Delete(entity);
            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<MediaReturnDto> UpdateAsync(int id, MediaUpdateDto dto)
        {
            var entity = await _unitOfWork.MediaRepository.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException(nameof(Media), id);

            _mapper.Map(dto, entity);
            _unitOfWork.MediaRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<MediaReturnDto>(entity);
        }
    }
}
