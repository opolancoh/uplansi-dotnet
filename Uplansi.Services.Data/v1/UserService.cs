using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Uplansi.Core.Contracts.Repositories;
using Uplansi.Core.Contracts.Services;
using Uplansi.Core.DTOs;
using Uplansi.Core.DTOs.User;
using Uplansi.Core.Entities.Account;

namespace Uplansi.Services.Data.v1;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserRepository _repository;

    public UserService(ILogger<UserService> logger, UserManager<ApplicationUser> userManager,
        IUserRepository repository)
    {
        _logger = logger;
        _userManager = userManager;
        _repository = repository;
    }

    public async Task<ApplicationResult> GetAll(PaginationOptions pagination)
    {
        var result = await _repository.GetAll(pagination);

        return new ApplicationResult
        {
            Status = ApplicationResultStatus.Ok,
            PageInfo = result.PageInfo,
            Data = result.Data
        };
    }

    public async Task<ApplicationResult?> GetById(Guid id)
    {
        var item = await _repository.GetById(id);

        if (item == null)
        {
            return new ApplicationResult
            {
                Status = ApplicationResultStatus.NotFound,
                Message = $"The item with id '{id}' was not found or you don't have permission to access it."
            };
        }

        return new ApplicationResult
        {
            Status = ApplicationResultStatus.Ok,
            Data = item
        };
    }

    public async Task<ApplicationResult> Add(UserAddModel item, Guid? authenticatedUserId)
    {
        var newItemId = Guid.NewGuid();
        var newItem = new ApplicationUser
        {
            Id = newItemId,
            UserName = item.UserName,
            FullName = item.FullName,
            DisplayName = item.DisplayName,
            Email = item.Email,
            LanguageId = item.Language,
            CountryId = item.Country,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedById = authenticatedUserId ?? newItemId,
            UpdatedById = authenticatedUserId ?? newItemId,
        };

        var userCreationResult = await _userManager.CreateAsync(newItem, item.Password);

        if (!userCreationResult.Succeeded)
        {
            return HandleError("An error has occurred while creating the User.", userCreationResult.Errors);
        }

        if (item.Roles != null && item.Roles.Any())
        {
            var rolesCreationResult = await _userManager.AddToRolesAsync(newItem, item.Roles);
            if (!rolesCreationResult.Succeeded)
            {
                return HandleError("An error has occurred while assigning roles to the User.",
                    rolesCreationResult.Errors);
            }
        }

        return new ApplicationResult
        {
            Status = ApplicationResultStatus.Created,
            Message = "Item created successfully.",
            Data = new { newItem.Id }
        };
    }

    public async Task<ApplicationResult> Update(Guid id, UserUpdateModel item, Guid authenticatedUserId)
    {
        // Look for current user
        var currentUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (currentUser == null)
        {
            return new ApplicationResult
            {
                Status = ApplicationResultStatus.NotFound,
                Message = $"User with Id '{id}' was not found.",
            };
        }

        // Update user data
        currentUser.FullName = item.FullName;
        currentUser.DisplayName = item.DisplayName;
        currentUser.Email = item.Email;
        currentUser.LanguageId = item.Language;
        currentUser.CountryId = item.Country;
        currentUser.UpdatedAt = DateTime.UtcNow;
        currentUser.UpdatedById = authenticatedUserId;

        // Update user
        var updateResult = await _userManager.UpdateAsync(currentUser);
        if (!updateResult.Succeeded)
        {
            return HandleError("An error has occurred while updating the user.", updateResult.Errors);
        }

        if (item.Roles != null)
        {
            // Update user roles
            var currentRoles = await _userManager.GetRolesAsync(currentUser);
            var rolesToAdd = item.Roles.Except(currentRoles);
            var rolesToRemove = currentRoles.Except(item.Roles);

            var addRolesResult = await _userManager.AddToRolesAsync(currentUser, rolesToAdd);
            if (!addRolesResult.Succeeded)
            {
                return HandleError("An error has occurred while updating the user roles.", addRolesResult.Errors);
            }

            var removeRolesResult = await _userManager.RemoveFromRolesAsync(currentUser, rolesToRemove);
            if (!removeRolesResult.Succeeded)
            {
                return HandleError("An error has occurred while updating the user roles.", removeRolesResult.Errors);
            }
        }

        return new ApplicationResult
        {
            Status = ApplicationResultStatus.NoContent,
            Message = "User updated successfully."
        };
    }

    public async Task<ApplicationResult> Remove(Guid id)
    {
        var currentUser = await _userManager
            .Users
            .Select(x =>
                new ApplicationUser
                {
                    Id = x.Id,
                    ConcurrencyStamp = x.ConcurrencyStamp
                })
            .FirstOrDefaultAsync(x => x.Id == id);

        if (currentUser == null)
        {
            return new ApplicationResult
            {
                Status = ApplicationResultStatus.NotFound,
                Message = $"User Id '{id}' was not found.",
            };
        }

        var result = await _userManager.DeleteAsync(currentUser);
        if (result.Succeeded)
        {
            return new ApplicationResult
            {
                Status = ApplicationResultStatus.NoContent,
                Message = "User deleted successfully.",
            };
        }

        return HandleError($"An error has occurred while deleting the User with Id '{id}'.", result.Errors);
    }

    private ApplicationResult HandleError(string message, IEnumerable<IdentityError> errors)
    {
        var errorList = errors.Select(error =>
        {
            _logger.LogError("Error. Code:{Code} Description:{Description}", error.Code, error.Description);
            return new ApplicationResultError { Code = error.Code, Description = error.Description };
        }).ToList();

        return new ApplicationResult
        {
            Status = ApplicationResultStatus.BadRequest,
            Message = message,
            Errors = errorList
        };
    }
}