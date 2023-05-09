using Grosvenor.Portal.Data;
using Grosvenor.Portal.Model;
using System;
using System.Threading.Tasks;

namespace Grosvenor.Portal.Business
{
    public class UserAccountManager : IUserAccountManager
    {
        private readonly IUserAccountRepository userAccountRepository;
        public UserAccountManager(IUserAccountRepository userAccountRepository) => this.userAccountRepository = userAccountRepository;
        public async Task<UserAccount> GetByIdAsync(Guid userAccountId) => await this.userAccountRepository.GetByIdAsync(userAccountId);
        public async Task<UserAccount> GetOrCreateAsync(UserAccount userAccount) => await this.userAccountRepository.GetOrCreateAsync(userAccount);
    }
}