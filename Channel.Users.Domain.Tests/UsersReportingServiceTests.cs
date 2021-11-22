using System.Collections.Generic;
using System.Linq;
using Channel.Users.Domain.DomainEntities.User;
using Channel.Users.Domain.Reporting.Users;
using Xunit;

namespace Channel.Users.Domain.Tests
{
    public class UsersReportingServiceTests
    {
        private readonly IList<User> _users;
        public UsersReportingServiceTests()
        {
            _users = new List<User>()
            {
                new User(30, "Paul", "Smith", 20, GenderOptions.Male),
                new User(45, "Paul", "Smith", 21, GenderOptions.Male),
                new User(30, "George", "Lucas", 22, GenderOptions.Male),
                new User(45, "Paola", "Smith", 21, GenderOptions.Female),
                new User(45, "Debbie", "Tsai", 21, GenderOptions.Female),
            };
        }

        [Fact]
        public void GetUsersById_TwoUsersWithThatId_BothAreReturned()
        {
            var reportingService = new UsersDataAggregationService();

            var filteredUsers = reportingService.GetUsersById(_users, 30).ToList();

            Assert.Equal(2, filteredUsers.Count());
            Assert.True(filteredUsers.Any(x => x.First == "Paul") == true);

        }

        [Fact]
        public void GetUsersCountByAgeAndGender()
        {
            var reportingService = new UsersDataAggregationService();

            var filteredUsers = reportingService.GetUsersCountByAgeAndGender(_users);

            Assert.Equal(2, filteredUsers
                .FirstOrDefault(x => x.Age == 21 && x.Gender == GenderOptions.Female)
                .Quantity);

        }

    }
}
