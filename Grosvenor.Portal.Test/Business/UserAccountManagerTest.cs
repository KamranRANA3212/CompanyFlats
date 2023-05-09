using Moq;
using Grosvenor.Portal.Business;
using Grosvenor.Portal.Data;
using Grosvenor.Portal.Model;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Grosvenor.Portal.Test.Business
{
    public class UserAccountManagerTest
    {
        private readonly IUserAccountManager manager;
        private readonly Mock<IUserAccountRepository> repository;
        public UserAccountManagerTest()
        {
            this.repository = new Mock<IUserAccountRepository>();
            this.manager = new UserAccountManager(this.repository.Object);
        }

        [Fact]
        public async Task GetByIdAsync()
        {
            //Arrange
            this.repository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) => new UserAccount { UserAccountId = id });

            //Act
            var id = Guid.NewGuid();
            var response = await this.manager.GetByIdAsync(id);

            //Assert
            this.repository.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()));
            Assert.NotNull(response);
            Assert.Equal(id, response.UserAccountId);
        }
    }
}