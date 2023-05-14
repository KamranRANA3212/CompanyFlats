using Microsoft.EntityFrameworkCore;
using Grosvenor.Portal.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grosvenor.Portal.Test.Data
{
    public abstract class RepositoryBase
    {
        protected static PortalContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<PortalContext>()
                .UseInMemoryDatabase(databaseName: "InMemory")
                .Options;

            return new PortalContext(options);
        }
    }
}
