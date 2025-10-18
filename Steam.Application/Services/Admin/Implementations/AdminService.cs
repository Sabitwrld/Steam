using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Admin;
using Steam.Application.DTOs.Auth;
using Steam.Application.Services.Admin.Interfaces;
using Steam.Domain.Entities.Identity;
using Steam.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.Admin.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<UserDto>> GetUsersWithRolesAsync()
        {
            var users = await _userManager.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    Roles = new List<string>() // Rolları ayrıca yükləyəcəyik
                })
                .ToListAsync();

            foreach (var userDto in users)
            {
                var user = await _userManager.FindByIdAsync(userDto.Id);
                if (user != null)
                {
                    userDto.Roles = await _userManager.GetRolesAsync(user);
                }
            }
            return users;
        }

        public async Task<List<IdentityRole>> GetRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task AssignRolesAsync(string userId, AssignRolesRequestDto request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found."); // Və ya NotFoundException
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to remove existing roles."); // Və ya custom exception
            }

            if (request.Roles != null && request.Roles.Any())
            {
                result = await _userManager.AddToRolesAsync(user, request.Roles);
                if (!result.Succeeded)
                {
                    throw new Exception("Failed to add new roles."); // Və ya custom exception
                }
            }
        }

        public async Task<DashboardStatsDto> GetDashboardStatisticsAsync()
        {
            var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);

            var completedOrders = await _unitOfWork.OrderRepository.GetAllAsync(predicate: o => o.Status == "Completed");
            var totalRevenue = completedOrders.Sum(o => o.TotalPrice);

            var newUsersCount = await _userManager.Users.CountAsync(u => u.CreatedAt >= thirtyDaysAgo);

            var allOrders = await _unitOfWork.OrderRepository.GetAllAsync();
            var totalOrdersCount = allOrders.Count;

            // Bu sətir artıq düzgün işləyəcək
            var pendingReviews = await _unitOfWork.ReviewRepository.GetAllAsync(predicate: r => !r.IsApproved);
            var pendingReviewsCount = pendingReviews.Count;

            return new DashboardStatsDto
            {
                TotalRevenue = totalRevenue,
                NewUsersLast30Days = newUsersCount,
                TotalOrders = totalOrdersCount,
                PendingReviews = pendingReviewsCount
            };
        }
    }
}
