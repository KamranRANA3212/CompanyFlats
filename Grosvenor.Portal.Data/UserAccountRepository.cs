using Microsoft.EntityFrameworkCore;
using Grosvenor.Portal.Data.Context;
using Grosvenor.Portal.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Grosvenor.Portal.Data
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly PortalContext context;
        public UserAccountRepository(PortalContext context) => this.context = context;
        public async Task<UserAccount> GetOrCreateAsync(UserAccount userAccount)
        {
            var existingUser = await context.UserAccounts.FirstOrDefaultAsync(x => x.UserAccountId == userAccount.UserAccountId || x.Email == userAccount.Email);

            if (existingUser != null)
            {
                if (existingUser.Name != userAccount.Name || existingUser.Email != userAccount.Email)
                {
                    existingUser.Email = userAccount.Email;
                    existingUser.Name = userAccount.Name;
                    _ = await context.SaveAsync();
                }

                return existingUser;
            }

            context.Add(userAccount);
            _ = await context.SaveAsync();

            return userAccount;
        }

        public async Task<UserAccount> GetByIdAsync(Guid userAccountId)
        {
            var userAccount = await context.UserAccounts.FindAsync(userAccountId);
            return userAccount;
        }
    }
}