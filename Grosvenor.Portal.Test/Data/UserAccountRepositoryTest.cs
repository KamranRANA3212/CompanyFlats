using Grosvenor.Portal.Data;
using Grosvenor.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Grosvenor.Portal.Test.Data
{
    public class UserAccountRepositoryTest : RepositoryBase
    {
        [Fact]
        public async Task CreateAsync()
        {
            //Arrange
            using var context = CreateContext();
            var repoistory = new UserAccountRepository(context);
            var user = new UserAccount
            {
                UserAccountId = Guid.NewGuid(),
                Email = "dev.team@stallions.tech",
                Name = "Dev Team"
            };

            //Act
            await repoistory.GetOrCreateAsync(user);

            //Assert
            using var testContext = CreateContext();
            Assert.True(testContext.UserAccounts.Any());

        }

        [Fact]
        public async Task GetAsync()
        {
            //Arrange
            using var context = CreateContext();
            var repoistory = new UserAccountRepository(context);
            Guid id = Guid.NewGuid();
            var user = new UserAccount
            {
                UserAccountId = id,
                Email = "dev.team@stallions.tech",
                Name = "Dev Team"
            };

            //Act
            await repoistory.GetOrCreateAsync(user);

            //Assert
            using var testContext = CreateContext();
            Assert.True(testContext.UserAccounts.Any());
        }
    }
}
