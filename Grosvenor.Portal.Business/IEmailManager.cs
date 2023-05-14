using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Grosvenor.Portal.Business
{
    public interface IEmailManager
    {
        Task<bool> SendAsync(string toEmailAddressess, string subject, string body);
    }
}
