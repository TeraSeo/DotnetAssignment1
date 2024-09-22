using System.Text;
using DotnetAssignment1.extensions;
using DotnetAssignment1.models;

namespace DotnetAssignment1.menus;

public class LoginMenu
{
    private static List<User> users = [];
    public void Authenticate() // This function shows Login menu and enable users to login
    {
        Console.Clear();
        Console.WriteLine("=======================================");
        Console.WriteLine("   DOTNET Hospital Management System   ");
        Console.WriteLine("=======================================");
        Console.WriteLine("                 Login                 ");
        Console.WriteLine();

        User loggedInUser = null;
        while (loggedInUser == null)
        {
            Console.Write("Enter your ID: ");
            string id = Console.ReadLine();
            
            Console.Write("Enter your Password: ");
            string password = ReadPassword();
            
            // Check if the entered ID and password match any user in the list
            loggedInUser = users.FirstOrDefault(user => user.Id == id && user.Password == password);

            if (loggedInUser != null)
            {
                Console.WriteLine("\nLogin successful. Welcome, " + loggedInUser.Id + "!\n");
            }
            else
            {
                Console.WriteLine("\nInvalid Id or Password\n");
            }
        }
        
        if (loggedInUser is Patient)
        {
            PatientMenu patientMenu = new PatientMenu();
            patientMenu.ShowOptions(loggedInUser as Patient); // show patient menu
        }
        else if (loggedInUser is Doctor)
        {
            DoctorMenu doctorMenu = new DoctorMenu();
            doctorMenu.ShowOptions(loggedInUser as Doctor); // show doctor menu
        }
        else if (loggedInUser is Administrator)
        {
            AdministratorMenu administratorMenu = new AdministratorMenu();
            administratorMenu.ShowOptions(); // show admin menu
        }
    }
    
    public void LoadUsers()
    {
        users = [];
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "textFiles", "users.txt");

        // Check if the file exists
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Error: The file 'users.txt' was not found at {filePath}");
            return;
        }
        
        string[] lines = File.ReadAllLines(filePath);
        for (int i = 1; i < lines.Length; i++)
        {
            var parts = lines[i].SplitAndValidate('/', 3); // use of extension method
            if (parts.Length == 0)
            {
                Console.WriteLine("Failed to Load user");
                return;
            }
            string id = parts[0];
            string password = parts[1];
            string role = parts[2];
            string name = "";
            string address = "";
            string email = "";
            string phone = "";
            if (parts.Length > 6) // prevent not to get these data for admin user
            {
                name = parts[3];
                address = parts[4];
                email = parts[5];
                phone = parts[6];
            }

            User user = null;
            switch (role)
            {
                case "Patient":
                    user = new Patient(id, password, name, address, email, phone);
                    break;
                case "Doctor":
                    user = new Doctor(id, password, name, address, email, phone);
                    break;
                case "Administrator":
                    user = new Administrator(id, password);
                    break;
            }
            users.Add(user);
        }
    }
    
    private string ReadPassword() // This function read encrypted password
    {
        StringBuilder password = new StringBuilder();
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(intercept: true);
            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                password.Append(key.KeyChar);
                Console.Write("*"); // Write * instead of Password
            }
            else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password.Length--;
                Console.Write("\b \b");
            }
        } while (key.Key != ConsoleKey.Enter);

        Console.WriteLine();
        return password.ToString();
    }
}