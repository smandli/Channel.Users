using System.Collections.Generic;
using Channel.Users.Domain.Reporting.Users;

namespace Channel.Users.Application.Commands.GetUsersReport
{
    public class GetUsersReportResponse
    {

        public string UsersFortyTwoNames { get; set; }

        public string UsersTwentyThreeOldFirstNames { get; set; }

        public IEnumerable<GenderAgeQuantity> GenderByAge { get; set; }

    }
}
