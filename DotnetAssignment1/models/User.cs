namespace DotnetAssignment1.models;

public class User
{
    public string Id { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }

    public User(string id, string password, string role)
    {
        Id = id;
        Password = password;
        Role = role;
    }
}
