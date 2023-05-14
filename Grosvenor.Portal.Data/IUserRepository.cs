using Grosvenor.Portal.Model;
using System;
using System.Threading.Tasks;

namespace Grosvenor.Portal.Data
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetOrCreateAsync(User user);
    }
}