using Grosvenor.Portal.Model;
using System;
using System.Threading.Tasks;

namespace Grosvenor.Portal.Data
{
    public interface IUserAccountRepository
    {
        Task<UserAccount> GetByIdAsync(Guid userAccountId);
        Task<UserAccount> GetOrCreateAsync(UserAccount userAccount);
    }
}