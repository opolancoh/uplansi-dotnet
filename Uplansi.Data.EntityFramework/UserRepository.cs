using Microsoft.EntityFrameworkCore;
using Uplansi.Core.Contracts.Repositories;
using Uplansi.Core.DTOs;
using Uplansi.Core.Entities.Account;

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
            .AsNoTracking()
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
                    CreatedAt = x.CreatedAt,
                    CreatedBy = new KeyValueDto<Guid>
                    {
                        Id = x.CreatedById,
                        Name = x.CreatedBy!.UserName!
                    },
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = new KeyValueDto<Guid>
                    {
                        Id = x.UpdatedById,
                        Name = x.UpdatedBy!.UserName!
                    },
                })
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

    /* public async Task<ApplicationUser?> GetById(string id)
    {
        return await _entitySet.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
    } */

    public async Task<int> Add(ApplicationUser item)
    {
        _entitySet.Add(item);
        return await _context.SaveChangesAsync();
    }

    /* public async Task Update(ApplicationUser item)
    {
        _context.Entry(item).State = EntityState.Modified;
        _context.Entry(item).Property(x => x.CreatedAt).IsModified = false;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ItemExists(item.Id))
            {
                throw new EntityNotFoundException<string>(item.Id);
            }
            else
            {
                throw;
            }
        }
    }

    public async Task Remove(string id)
    {
        var item = new ApplicationUser { Id = id };

        _entitySet.Remove(item);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ItemExists(id))
            {
                throw new EntityNotFoundException<string>(id);
            }
            else
            {
                throw;
            }
        }
    }

    public async Task<bool> ItemExists(string id)
    {
        return await _entitySet.AnyAsync(e => e.Id == id);
    }

    public async Task AddRange(IEnumerable<ApplicationUser> items)
    {
        await _entitySet.AddRangeAsync(items);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ApplicationUserListDto>> GetAllWithRoles()
    {
        var result = await (from u in _context.Users
            join ur in _context.UserRoles on u.Id equals ur.UserId into userRolesGroup
            from ur in userRolesGroup.DefaultIfEmpty()
            join r in _context.Roles on ur.RoleId equals r.Id into rolesGroup
            from r in rolesGroup.DefaultIfEmpty()
            select new
            {
                u.Id,
                u.FirstName,
                u.LastName,
                u.UserName,
                Role = r.Name
            }).AsNoTracking().ToListAsync();

        var resultWithListOfRoles =
            result
                .GroupBy(x => new
                {
                    x.Id,
                    x.FirstName,
                    x.LastName,
                    x.UserName,
                })
                .Select(x => new ApplicationUserListDto
                {
                    Id = x.Key.Id,
                    FirstName = x.Key.FirstName,
                    LastName = x.Key.LastName,
                    UserName = x.Key.UserName,
                    Roles = (x.Count() == 1 && x.FirstOrDefault()?.Role == null)
                        ? new List<string>()
                        : x.Select(r => r.Role).ToList()!
                });

        return resultWithListOfRoles;
    }

    public async Task<ApplicationUserDetailsDto?> GetByIdWithRoles(string id)
    {
        var result = await (
            from u in _context.Users
            join ur in _context.UserRoles on u.Id equals ur.UserId into userRolesGroup
            from ur in userRolesGroup.DefaultIfEmpty()
            join r in _context.Roles on ur.RoleId equals r.Id into rolesGroup
            from r in rolesGroup.DefaultIfEmpty()
            where u.Id == id
            select new
            {
                u.Id,
                u.FirstName,
                u.LastName,
                u.UserName,
                u.CreatedAt,
                u.UpdatedAt,
                Role = r.Name
            }).AsNoTracking().ToListAsync();

        var resultWithListOfRoles =
            result
                .GroupBy(x => new
                {
                    x.Id,
                    x.FirstName,
                    x.LastName,
                    x.UserName,
                    x.CreatedAt,
                    x.UpdatedAt
                })
                .Select(x => new ApplicationUserDetailsDto
                {
                    Id = x.Key.Id,
                    FirstName = x.Key.FirstName,
                    LastName = x.Key.LastName,
                    UserName = x.Key.UserName,
                    CreatedAt = x.Key.CreatedAt,
                    UpdatedAt = x.Key.UpdatedAt,
                    Roles = (x.Count() == 1 && x.FirstOrDefault()?.Role == null)
                        ? new List<string>()
                        : x.Select(r => r.Role).ToList()!
                });

        return resultWithListOfRoles.SingleOrDefault();
    } */
}