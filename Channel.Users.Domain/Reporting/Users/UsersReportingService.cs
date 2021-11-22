using System.Collections.Generic;
using System.Linq;

namespace Channel.Users.Domain.Reporting.Users
{
    public class UsersReportingService : IUsersReportingService
    {

        public IEnumerable<User> GetUsersById(IList<User> users, int id)
        {
            return users
                .Where(x => x.Id == id);
        }

        public IEnumerable<User> GetUsersByAge(IList<User> users, int age)
        {
            return users
                .Where(x => x.Age == age);
        }

        public IEnumerable<GenderAgeQuantity> GetUsersCountByAgeAndGender(IList<User> users)
        {
            return
                users
                    .GroupBy(x => new
                    {
                        x.Age,
                        x.Gender
                    })
                    .Select(x => new GenderAgeQuantity
                    {
                        Gender = x.Key.Gender,
                        Age = x.Key.Age,
                        Quantity = x.Count()
                    });
        }

    }
}