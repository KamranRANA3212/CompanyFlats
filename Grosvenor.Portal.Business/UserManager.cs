using Grosvenor.Portal.Data;
using Grosvenor.Portal.Model;
using System;
using System.Threading.Tasks;

namespace Grosvenor.Portal.Business
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository userRepository;
        public UserManager(IUserRepository userRepository) => this.userRepository = userRepository;
        public async Task<User> GetByIdAsync(Guid id) => await this.userRepository.GetByIdAsync(id);
        public async Task<User> GetOrCreateAsync(User user) => await this.userRepository.GetOrCreateAsync(user);
    }
}