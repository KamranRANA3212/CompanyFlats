using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Grosvenor.Portal.Model;

namespace Grosvenor.Portal.Data.EntityConfiguration
{
    internal class UserAccountEntityConfiguration : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.ToTable(nameof(UserAccount));
            builder.HasKey(c => c.UserAccountId);
            builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Email).HasMaxLength(150).IsRequired();
        }
    }
}
