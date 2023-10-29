using Microsoft.EntityFrameworkCore;
using Uplansi.Core.Contracts.Repositories;
using Uplansi.Core.DTOs;
using Uplansi.Core.DTOs.User;
using Uplansi.Core.Entities.Account;
using Uplansi.Core.Exceptions;

namespace Uplansi.Data.EntityFramework;

public class UserRepository : IUserRepository
{
    private readonly AccountDbContext _context;
    private readonly DbSet<ApplicationUser> _entitySet;

    public UserRepository(AccountDbContext context)
    {
        _context = context;
        _entitySet = context.Users;
    }

    public async Task<PagedListResult<UserListResult>> GetAll(PaginationOptions pagination)
    {
        var (pageIndex, pageSize, skip, addTotalCount) = pagination;

        // Get the total count of records
        int? totalCount = null;
        if (addTotalCount)
        {
            totalCount = await _entitySet.CountAsync();
        }

        var data = await _entitySet
            .Skip(skip)
            .Take(pageSize)
            .Select(
                x => new UserListResult
                {
                    Id = x.Id,
                    UserName = x.UserName!,
                    FullName = x.FullName,
                    DisplayName = x.DisplayName,
                    Country = x.CountryId,
                    Language = x.LanguageId,
                    UpdatedAt = x.UpdatedAt
                })
            .AsNoTracking()
            .ToListAsync();

        return new PagedListResult<UserListResult>
        {
            Data = data,
            PageInfo = new PageInfo
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount
            }
        };
    }

    public async Task<UserDetailResult?> GetById(Guid id)
    {
        var item = await _entitySet
            .AsNoTracking()
            .Select(
                x => new UserDetailResult
                {
                    Id = x.Id,
                    UserName = x.UserName!,
                    FullName = x.FullName,
                    DisplayName = x.DisplayName,
                    Gender = x.Gender,
                    Country = x.CountryId,
                    Language = x.LanguageId,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = new KeyValueDto<Guid>
                    {
                        Id = x.CreatedBy!.Id,
                        Name = x.CreatedBy!.UserName!
                    },
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = new KeyValueDto<Guid>
                    {
                        Id = x.UpdatedBy!.Id,
                        Name = x.UpdatedBy!.UserName!
                    }
                })
            .SingleOrDefaultAsync(x => x.Id == id);

        if (item == null) return item;

        var roles =
            from ur in _context.UserRoles
            join r in _context.Roles on ur.RoleId equals r.Id
            where ur.UserId == id
            select r.Name;

        item.Roles = await roles.AsNoTracking().ToListAsync();

        return item;
    }

    public async Task<int> Add(ApplicationUser item)
    {
        _entitySet.Add(item);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> Update(ApplicationUser item)
    {
        _context.Entry(item).State = EntityState.Modified;
        _context.Entry(item).Property(x => x.CreatedAt).IsModified = false;

        try
        {
            return await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ItemExists(item.Id))
            {
                throw new RecordNotFoundException<Guid>(item.Id);
            }
            else
            {
                throw;
            }
        }
    }

    public async Task<bool> ItemExists(Guid id)
    {
        return await _entitySet.AsNoTracking().AnyAsync(e => e.Id == id);
    }
}