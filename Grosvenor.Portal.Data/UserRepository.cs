using Microsoft.EntityFrameworkCore;
using Grosvenor.Portal.Data.Context;
using Grosvenor.Portal.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Grosvenor.Portal.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly PortalContext context;
        public UserRepository(PortalContext context) => this.context = context;
        public async Task<User> GetOrCreateAsync(User user)
        {
            var existingUser = await context.Users.FindAsync(user.Id);

            if (existingUser != null)
            {
                if (existingUser.Name != user.Name || existingUser.Email != user.Email)
                {
                    existingUser.Email = user.Email;
                    existingUser.Name = user.Name;
                    await context.SaveAsync();
                }

                return existingUser;
            }

            user.CreatedDate = DateTime.UtcNow;
            user.UpdatedDate = DateTime.UtcNow;
            user.CreatedById = user.Id;
            user.UpdatedById = user.Id;

            context.Add(user);
            await context.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await context.Users.FindAsync(id);
            return user;
        }
    }
}