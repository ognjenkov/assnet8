using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _dbContext;
        public AccountService(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Task DeleteUserFromuserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Organization?> GetOrganizationFromUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Team?> GetTeamFromUserId(Guid userId)
        {
            return await _dbContext.Teams
                                    .Where(t => t.Memberships.Any(m => m.UserId == userId))
                                    .Include(t => t.Galleries)
                                        .ThenInclude(g => g.Images)
                                    .Include(t => t.Memberships)
                                    .SingleOrDefaultAsync();
        }

        public async Task<User?> GetAccountFromUserId(Guid userId)
        {
            return await _dbContext.Users
                                .Where(u => u.Id == userId)
                                .Include(u => u.Listings)
                                    .ThenInclude(l => l.ThumbnailImage)
                                .Include(u => u.Entries)
                                .Include(u => u.ProfileImage)
                                .Include(u => u.Membership!)
                                    .ThenInclude(m => m.Team!)
                                        .ThenInclude(t => t.LogoImage)
                                .SingleOrDefaultAsync();
        }

        public Task<User> UpdateUser(User newUserData)
        {
            throw new NotImplementedException();
        }
    }
}