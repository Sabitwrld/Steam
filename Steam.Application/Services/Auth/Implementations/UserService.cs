using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Auth;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Exceptions;
using Steam.Application.Services.Auth.Interfaces;
using Steam.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.Auth.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<PagedResponse<UserDto>> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            var query = _userManager.Users;
            var totalCount = await query.CountAsync();
            var users = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userDto = _mapper.Map<UserDto>(user);
                userDto.Roles = roles;
                userDtos.Add(userDto);
            }

            return new PagedResponse<UserDto>
            {
                Data = userDtos,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new NotFoundException(nameof(AppUser), userId);

            var roles = await _userManager.GetRolesAsync(user);
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Roles = roles;

            return userDto;
        }

        public async Task AssignRoleAsync(AssignRoleDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
                throw new NotFoundException(nameof(AppUser), dto.UserId);

            if (!await _roleManager.RoleExistsAsync(dto.RoleName))
                throw new NotFoundException("Role not found.");

            await _userManager.AddToRoleAsync(user, dto.RoleName);
        }

        public async Task RemoveRoleAsync(AssignRoleDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
                throw new NotFoundException(nameof(AppUser), dto.UserId);

            if (!await _roleManager.RoleExistsAsync(dto.RoleName))
                throw new NotFoundException("Role not found.");

            await _userManager.RemoveFromRoleAsync(user, dto.RoleName);
        }
    }
}
