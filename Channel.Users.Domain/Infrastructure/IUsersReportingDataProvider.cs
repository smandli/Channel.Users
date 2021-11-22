using System.Collections.Generic;
using System.Threading.Tasks;
using Channel.Users.Domain.Reporting.Users;

namespace Channel.Users.Domain.Infrastructure
{
    public interface IUsersReportingDataProvider
    {
        Task<IList<User>> GetUsers();

    }
}
