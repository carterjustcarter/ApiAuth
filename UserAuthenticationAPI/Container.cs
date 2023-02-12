using UserAuthenticationAPI.Model;

namespace UserAuthenticationAPI {
    public static class Container {
        private static List<User> _users = new List<User>() {
            new User() { Id=1, Name="Janusz", LastName="Kowalski", Password="password", AccessLevel=AccessLevel.ADMIN_ACCESS },
            new User() { Id=2, Name="Artur", LastName="Nowak", Password="password", AccessLevel=AccessLevel.BASIC_ACCESS },
            new User() { Id=3, Name="Adam", LastName="Nowakowski", Password = "password", AccessLevel=AccessLevel.NO_ACCESS }
        };

        public static List<User> GetUsers() {
            return _users;
        }

        public static void AddUser(User user) {
            _users.Add(user);
        }

        public static User GetUserById(int id) => _users.Find(x => x.Id == id);

        public static User GetUserByNameLastNamePassword(string name, string lastname, string password) {
            return _users.Find(x => x.Name == name
                                 && x.LastName == lastname 
                                 && x.Password == password);
        }
    }
}
