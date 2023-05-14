using Moq;
using Grosvenor.Portal.Business;
using Grosvenor.Portal.Data;
using Grosvenor.Portal.Model;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Grosvenor.Portal.Test.Business
{
    public class UserManagerTest
    {
        private readonly IUserManager manager;
        private readonly Mock<IUserRepository> repository;
        public UserManagerTest()
        {
            this.repository = new Mock<IUserRepository>();
            this.manager = new UserManager(this.repository.Object);
        }

        [Fact]
        public async Task GetByIdAsync()
        {
            //Arrange
            this.repository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) => new User
            {
                Id = id,
                Email = "dev.team@stallions.tech",
                Name = "Dev Team",
                CreatedById = id,
                CreatedDate = DateTime.UtcNow,
                UpdatedById = id,
                UpdatedDate = DateTime.UtcNow,
            });

            //Act
            var id = Guid.NewGuid();
            var response = await this.manager.GetByIdAsync(id);

            //Assert
            this.repository.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()));
            Assert.NotNull(response);
            Assert.Equal(id, response.Id);
        }
    }
}