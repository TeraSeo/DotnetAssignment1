namespace DotnetAssignment1.models;

public class Patient : User
{
    public string FullName { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    
    public Patient(string id, string password, string fullName, string address, string email, string phone)
        : base(id, password, "Patient")
    {
        FullName = fullName;
        Address = address;
        Email = email;
        Phone = phone;
    }
    
    public Patient(string id, string fullName, string address, string email, string phone)
        : base(id, "", "Patient")
    {
        FullName = fullName;
        Address = address;
        Email = email;
        Phone = phone;
    }
    
    public override string ToString()  // use of overriding
    {
        return $"ID: {Id}\n" +
               $"Full Name: {FullName}\n" +
               $"Address: {Address}\n" +
               $"Email: {Email}\n" +
               $"Phone: {Phone}\n";
    }
    
    public override bool Equals(object obj)
    {
        if (obj is Patient other)
        {
            return Id == other.Id &&
                   FullName == other.FullName &&
                   Address == other.Address &&
                   Email == other.Email &&
                   Phone == other.Phone;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, FullName, Address, Email, Phone);
    }
}