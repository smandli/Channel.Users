using System.Collections.Generic;

namespace Channel.Users.Domain.Reporting.Users
{
    public interface IUsersReportingService
    {
        IEnumerable<User> GetUsersById(IList<User> users, int id);

        IEnumerable<User> GetUsersByAge(IList<User> users, int age);

        IEnumerable<GenderAgeQuantity> GetUsersCountByAgeAndGender(IList<User> users);
    }
}
