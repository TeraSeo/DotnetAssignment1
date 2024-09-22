namespace DotnetAssignment1.models;

public class Doctor : User
{
    public string FullName { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public Doctor(string id, string password, string fullName, string address, string email, string phone) : base(id,
        password, "Doctor")
    {
        FullName = fullName;
        Address = address;
        Email = email;
        Phone = phone;
    }
    
    public Doctor(string id, string fullName, string address, string email, string phone)
        : base(id, "", "Patient")
    {
        FullName = fullName;
        Address = address;
        Email = email;
        Phone = phone;
    }
    
    public override string ToString() // use of overriding
    {
        return $"ID: {Id}\n" +
               $"Full Name: {FullName}\n" +
               $"Address: {Address}\n" +
               $"Email: {Email}\n" +
               $"Phone: {Phone}\n";
    }
}