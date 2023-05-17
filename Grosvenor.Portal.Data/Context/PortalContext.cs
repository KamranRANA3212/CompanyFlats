using Microsoft.EntityFrameworkCore;
using Grosvenor.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Grosvenor.Portal.Data.Context
{
    public class PortalContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        #region Infrastructure
        public PortalContext(DbContextOptions<PortalContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) { modelBuilder.ApplyConfigurationsFromAssembly(typeof(PortalContext).Assembly); }
        public virtual async Task<bool> SaveAsync()
        {
            var affactedRows = await base.SaveChangesAsync();
            return affactedRows > 0;
        }
        public virtual void SetAdded(object entity) => this.Entry(entity).State = EntityState.Added;
        public virtual void SetDeleted(object entity) => this.Entry(entity).State = EntityState.Deleted;
        public virtual void SetModified<TEntity>(TEntity entity, params Expression<Func<TEntity, object>>[] properties) where TEntity : class
        {
            var fields = new List<string>();
            foreach (var p in properties)
            {
                var expression = (MemberExpression)((UnaryExpression)p.Body).Operand;
                string memberName = expression.Member.Name;
                fields.Add(memberName);
            }

            SetModified(entity, fields.ToArray());
        }
        public virtual void SetModified(object entity, params string[] properties)
        {
            var entry = this.Entry(entity);
            if (properties.Length == 0)
            {
                entry.State = EntityState.Modified;
                return;
            }

            entry.State = EntityState.Unchanged;
            foreach (var property in properties)
            {
                entry.Property(property).IsModified = true;
            }
        }
        #endregion
    }
}
