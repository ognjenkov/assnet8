using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Services.Account
{
    public interface IAccountService
    {
        public Task<User?> GetAccountFromUserId(Guid userId);
        public Task<Organization?> GetOrganizationFromUserId(Guid userId);
        public Task<Team?> GetTeamFromUserId(Guid userId);
        public Task DeleteUserFromuserId(Guid userId);
        public Task<User> UpdateUser(User newUserData);
    }
}