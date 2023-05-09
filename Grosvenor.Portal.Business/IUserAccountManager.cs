using Grosvenor.Portal.Model;
using System;
using System.Threading.Tasks;

namespace Grosvenor.Portal.Business
{
    public interface IUserAccountManager
    {
        Task<UserAccount> GetByIdAsync(Guid userAccountId);
        Task<UserAccount> GetOrCreateAsync(UserAccount userAccount);
    }
}