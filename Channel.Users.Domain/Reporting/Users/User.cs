
namespace Channel.Users.Domain.Reporting.Users
{
    public class User
    {
        public User(int id, string first, string last, int age, GenderOptions? gender)
        {
            Id = id;
            First = first;
            Last = last;
            Age = age;
            Gender = gender;
        }

        public int Id { get; }

        public string First { get; }

        public string Last { get; }

        public int Age { get; }

        public GenderOptions? Gender { get; }

        public string FullName => $"{First} {Last}";
    }
}
