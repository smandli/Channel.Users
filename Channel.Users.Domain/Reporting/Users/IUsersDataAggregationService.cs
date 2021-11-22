using System.Collections.Generic;
using Channel.Users.Domain.DomainEntities.User;

namespace Channel.Users.Domain.Reporting.Users
{
    public interface IUsersDataAggregationService
    {
        IEnumerable<User> GetUsersById(IList<User> users, int id);

        IEnumerable<User> GetUsersByAge(IList<User> users, int age);

        IEnumerable<GenderAgeQuantity> GetUsersCountByAgeAndGender(IList<User> users);
    }
}
