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
    public class UserRepositoryTest : RepositoryBase
    {
        [Fact]
        public async Task CreateAsync()
        {
            //Arrange
            using var context = CreateContext();
            var repoistory = new UserRepository(context);
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "dev.team@stallions.tech",
                Name = "Dev Team"
            };

            //Act
            await repoistory.GetOrCreateAsync(user);

            //Assert
            using var testContext = CreateContext();
            Assert.True(testContext.Users.Any());

        }

        [Fact]
        public async Task GetByIdAsync()
        {
            //Arrange
            using var context = CreateContext();
            var repoistory = new UserRepository(context);
            var id = Guid.NewGuid();
            var model = new User
            {
                Id = id,
                Email = "dev.team@stallions.tech",
                Name = "Dev Team",
                CreatedById = id,
                CreatedDate = DateTime.UtcNow,
                UpdatedById = id,
                UpdatedDate = DateTime.UtcNow,
            };

            await repoistory.GetOrCreateAsync(model);

            //Act
            var user = await repoistory.GetByIdAsync(id);

            //Assert
            Assert.NotNull(user);
            Assert.Equal(id, user.Id);  
        }
    }
}
