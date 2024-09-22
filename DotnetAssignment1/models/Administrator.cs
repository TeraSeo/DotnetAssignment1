namespace DotnetAssignment1.models;

public class Administrator : User
{
    public Administrator(string id, string password) : base(id, password, "Administrator") { }
}