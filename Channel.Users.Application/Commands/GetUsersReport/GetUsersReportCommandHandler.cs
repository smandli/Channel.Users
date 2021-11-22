using System;
using System.Linq;
using System.Threading.Tasks;
using Channel.Users.Application.Commands.Abstractions;
using Channel.Users.Domain.Infrastructure;
using Channel.Users.Domain.Reporting.Users;
using Microsoft.Extensions.Logging;

namespace Channel.Users.Application.Commands.GetUsersReport
{
    public class GetUsersReportCommandHandler : ICommandHandler<GetUsersReportRequest, GetUsersReportResponse>
    {
        private readonly IUsersDataAggregationService _usersDataAggregationService;
        private readonly IUsersDataProvider _usersDataProvider;
        private readonly ILogger<GetUsersReportCommandHandler> _logger;

        public GetUsersReportCommandHandler(IUsersDataAggregationService usersDataAggregationService, IUsersDataProvider usersDataProvider, ILogger<GetUsersReportCommandHandler> logger)
        {
            _usersDataAggregationService = usersDataAggregationService;
            _usersDataProvider = usersDataProvider;
            _logger = logger;
        }

        /// <summary>
        /// Fetches the user data and calculates specific data points.
        /// Returns data for display to the client.
        /// </summary>
        public async Task<GetUsersReportResponse> Handle(GetUsersReportRequest request)
        {
            try
            {
                _logger.LogInformation("Fetching users data...");

                var users = await _usersDataProvider.GetUsers();


                _logger.LogInformation("Generating reports...");

                var response = new GetUsersReportResponse();

                // Get users' full name for id = 42
                var usersById = _usersDataAggregationService.GetUsersById(users, 42);

                response.UsersFortyTwoNames = string.Join(",", 
                    usersById.Select(x => x.FullName));

                // All users first names (comma separated) who are 23
                var usersByAge = _usersDataAggregationService.GetUsersByAge(users, 23);

                response.UsersTwentyThreeOldFirstNames = string.Join(", ", 
                    usersByAge.Select(x => x.First));

                // The number of genders per Age, displayed from youngest to oldest
                response.GenderByAge = _usersDataAggregationService.GetUsersCountByAgeAndGender(users);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error while generating the users report. Please try again later", ex);
                throw;
            }
        }
    }
}
