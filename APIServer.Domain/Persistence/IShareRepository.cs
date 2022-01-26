using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.Domain.Persistence
{
    public interface IShareRepository
    {
        bool SendEmailAsync(EmailModel message);
    }
}
