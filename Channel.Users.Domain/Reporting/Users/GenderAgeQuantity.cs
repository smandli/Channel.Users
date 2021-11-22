using Channel.Users.Domain.DomainEntities.User;

namespace Channel.Users.Domain.Reporting.Users
{
    public struct GenderAgeQuantity
    {
        public int Age { get; set; }

        public GenderOptions? Gender { get; set; }

        public int Quantity { get; set; }

    }
}
