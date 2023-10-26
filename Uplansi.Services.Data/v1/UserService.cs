using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Uplansi.Core.Contracts.Repositories;
using Uplansi.Core.Contracts.Services;
using Uplansi.Core.DTOs;
using Uplansi.Core.Entities;
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

    /* public async Task<ApplicationResult?> GetById(Guid id)
    {
        var item = await _repository.GetById(id);

        if (item == null)
        {
            return new ApplicationResult
            {
                Status = 404,
                Message = $"The item with id '{id}' was not found or you don't have permission to access it."
            };
        }

        return new ApplicationResult
        {
            Status = 200,
            Data = item
        };
    } */

    public async Task<ApplicationResult> Add(UserAddOrUpdate item)
    {
        var newItemId = Guid.NewGuid();
        var newItem = new ApplicationUser
        {
            Id = newItemId,
            UserName = item.Username,
            FullName = item.FullName,
            DisplayName = item.DisplayName,
            LanguageId = item.LanguageId,
            CountryId = item.CountryId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedById = item.AuthenticatedUserId ?? newItemId,
            UpdatedById = item.AuthenticatedUserId ?? newItemId,
        };

        var result = await _userManager.CreateAsync(newItem, item.Password);

        if (result.Succeeded)
        {
            return new ApplicationResult
            {
                Status = ApplicationResultStatus.Created,
                Message = "Item created successfully.",
                Data = new { newItem.Id }
            };
        }

        var errors = result.Errors.Select(error =>
        {
            _logger.LogError("User not created. Code:{Code} Description:{Description}", error.Code, error.Description);
            return new ApplicationResultError { Code = error.Code, Description = error.Description };
        }).ToList();

        return new ApplicationResult
        {
            Status = ApplicationResultStatus.BadRequest,
            Message = "An error has occurred while creating the User.",
            Errors = errors
        };

        /* var passwordHasher = new PasswordHasher<ApplicationUser>();
        newItem.PasswordHash = passwordHasher.HashPassword(newItem, item.Password);

        var affectedRows = await _repository.Add(newItem);

        return new ApplicationResult
        {
            Status = ApplicationResultStatus.Created,
            Message = "Item created successfully.",
            Data = new { newItem.Id }
        }; */
    }
}