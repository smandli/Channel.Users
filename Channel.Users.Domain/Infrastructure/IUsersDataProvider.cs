using System.Collections.Generic;
using System.Threading.Tasks;
using Channel.Users.Domain.DomainEntities.User;

namespace Channel.Users.Domain.Infrastructure
{
    public interface IUsersDataProvider
    {
        Task<IList<User>> GetUsers();

    }
}
