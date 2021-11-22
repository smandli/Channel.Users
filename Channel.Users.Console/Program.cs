using System;
using System.Linq;
using System.Threading.Tasks;
using Channel.Users.Application.Commands.Abstractions;
using Channel.Users.Application.Commands.GetUsersReport;
using Microsoft.Extensions.DependencyInjection;

namespace Channel.Users.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var host = Startup.CreateHostBuilder(args).Build();

            if (args.Length < 1)
                throw new SystemException("Please provide the command to execute, i.e. get-users-report.");

            var commandName = args[0];

            switch (commandName)
            {
                case "get-users-report":
                    await SendGetUsersReportRequest(host.Services);
                    break;
                default:
                    throw new ArgumentException($"The command name {commandName} is not supported. Try with get-users-report.");
            }

        }

        private static async Task SendGetUsersReportRequest(IServiceProvider services)
        {
            var service = services.GetRequiredService<ICommandHandler<GetUsersReportRequest, GetUsersReportResponse>>();
            var response = await service.Handle(new GetUsersReportRequest());

            // Displaying results

            System.Console.WriteLine("Results: ");

            System.Console.WriteLine($"   Users with Id = 42: {response.UsersFortyTwoNames}");
            System.Console.WriteLine($"   Users' first names aged 23: {response.UsersTwentyThreeOldFirstNames}");

            if (response.GenderByAge == null)
                return;

            System.Console.WriteLine($"   Genders per age:");
            var lastAge = -1;
            foreach (var genderAge in response.GenderByAge.OrderBy(x => x.Age)
                .ThenByDescending(x => x.Gender))
            {
                if (genderAge.Age != lastAge)
                {
                    lastAge = genderAge.Age;
                    System.Console.Write($"\n     Age: {genderAge.Age}");
                }

                System.Console.Write($" {genderAge.Gender?.ToString() ?? "Others/Unknown"}:{genderAge.Quantity}");
            }

        }


    }
}
