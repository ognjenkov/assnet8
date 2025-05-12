using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Services.Account;

public class AccountService : IAccountService
{
    private readonly AppDbContext _dbContext;
    public AccountService(AppDbContext dbContext) // TODO ceo kod ovde mora da se sredi ovo je boze pomozi
    {
        this._dbContext = dbContext;
    }

    public Task DeleteUserFromuserId(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Organization?> GetOrganizationFromUserIdOrTeamId(Guid guid)
    {
        return await _dbContext.Organizations
                                .Where(o => o.TeamId == guid || o.UserId == guid)
                                .AsSplitQuery()
                                .Include(o => o.LogoImage)
                                .Include(o => o.Team!)
                                    .ThenInclude(t => t.LogoImage)
                                .Include(o => o.Fields)
                                .ThenInclude(f => f.ThumbnailImage)
                                .Include(o => o.Games)
                                .Include(o => o.Services)
                                .ThenInclude(s => s.ThumbnailImage)
                                .Include(o => o.User)
                                    .ThenInclude(u => u!.ProfileImage)
                                .SingleOrDefaultAsync();
    }

    public async Task<Team?> GetTeamFromUserId(Guid userId)
    {
        return await _dbContext.Teams
                                .Where(t => t.Memberships.Any(m => m.UserId == userId))
                                .AsSplitQuery()
                                .Include(t => t.Creator!)
                                    .ThenInclude(u => u.ProfileImage)
                                .Include(t => t.LogoImage)
                                .Include(t => t.Memberships)
                                    .ThenInclude(m => m.Roles!)
                                .Include(t => t.Memberships)
                                    .ThenInclude(m => m.User!)
                                        .ThenInclude(u => u.ProfileImage)
                                .Include(t => t.Location)
                                .Include(t => t.Galleries)
                                    .ThenInclude(g => g.Images)
                                .Include(t => t.Galleries)
                                    .ThenInclude(g => g.User)
                                .SingleOrDefaultAsync();
    }

    public async Task<User?> GetAccountFromUserId(Guid userId)
    {
        return await _dbContext.Users
                            .Where(u => u.Id == userId)
                            .AsSplitQuery()
                            .Include(u => u.Listings)
                                .ThenInclude(l => l.ThumbnailImage)
                            .Include(u => u.Entries)
                            .Include(u => u.ProfileImage)
                            .Include(u => u.Membership)
                                .ThenInclude(m => m!.Team)
                                    .ThenInclude(t => t!.LogoImage)
                            .Include(u => u.Membership)
                                .ThenInclude(m => m!.Team)
                                    .ThenInclude(t => t!.Organization)
                            .Include(u => u.Membership)
                                .ThenInclude(m => m!.Roles)
                            .Include(u => u.Organization)
                            .SingleOrDefaultAsync();  // NEMA ROLES
                                                      //     return await _dbContext.Users
                                                      // .Where(u => u.Id == userId)
                                                      // .Select(u => new User
                                                      // {
                                                      //     Id = u.Id,
                                                      //     Listings = u.Listings.Select(l => new Listing
                                                      //     {
                                                      //         Id = l.Id,
                                                      //         ThumbnailImage = l.ThumbnailImage
                                                      //     }).ToList(),
                                                      //     Entries = u.Entries,
                                                      //     ProfileImage = u.ProfileImage,
                                                      //     Membership = u.Membership == null ? null : new Membership
                                                      //     {
                                                      //         Id = u.Membership.Id,
                                                      //         Team = u.Membership.Team == null ? null : new Team
                                                      //         {
                                                      //             Id = u.Membership.Team.Id,
                                                      //             Name = u.Membership.Team.Name,
                                                      //             LogoImage = u.Membership.Team.LogoImage
                                                      //         },
                                                      //         Roles = u.Membership.Roles.Select(r => new Role
                                                      //         {
                                                      //             Id = r.Id,
                                                      //             Name = r.Name
                                                      //         }).ToList()
                                                      //     }
                                                      // })
                                                      // .SingleOrDefaultAsync(); OVDE KAO MOGU DA SE NAMESTE ROLES ALI ME MRZI
    }

    public Task<User> UpdateUser(User newUserData)
    {
        throw new NotImplementedException();
    }
}