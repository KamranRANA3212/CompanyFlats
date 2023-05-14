using Grosvenor.Portal.Model;
using System;
using System.Threading.Tasks;

namespace Grosvenor.Portal.Business
{
    public interface IUserManager
    {
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetOrCreateAsync(User user);
    }
}