namespace UserAuthenticationAPI.Model {

    public enum AccessLevel {
        NO_ACCESS = 0,
        BASIC_ACCESS = 1,
        MANAGER_ACCESS = 2,
        ADMIN_ACCESS = 3
    }

    public class User {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public AccessLevel AccessLevel { get; set; }
    }
}
