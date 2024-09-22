using DotnetAssignment1.extensions;
using DotnetAssignment1.menus;
using DotnetAssignment1.models;

namespace DotnetAssignment1.services;

public class PatientService
{
    public Patient? LoadPatientByPatientId(string patientId) // Load patient details by patient id
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "textFiles", "users.txt");

        // Check if the file exists
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Error: The file 'users.txt' was not found at {filePath}");
        }
        
        string[] lines = File.ReadAllLines(filePath);
        for (int i = 1; i < lines.Length; i++)
        {
            var parts = lines[i].Split('/');
            string id = parts[0];
            string role = parts[2];
          
            switch (role)
            {
                case "Patient":
                    if (id == patientId)
                    {
                        string name = parts[3];
                        string address = parts[4];
                        string email = parts[5];
                        string phone = parts[6];
                        return new Patient(id, name, address, email, phone);
                    }
                    break;
            }
        }
        return null;
    }
    
    public List<Patient> LoadPatientsByDoctorId(string doctorId) // Load patients by doctor id
    {
        List<Patient> patients = [];
        
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "textFiles", "appointments.txt");

        // Check if the file exists
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Error: The file 'appointments.txt' was not found at {filePath}");
            return patients;
        }
        
        string[] lines = File.ReadAllLines(filePath);

        for (int i = 1; i < lines.Length; i++)
        {
            if (lines[i] != "" && lines[i].Contains("/"))
            {
                var parts = lines[i].Split('/');
                if (parts.Length > 2)
                {
                    string patientId = parts[0];
                    string _doctorId = parts[2];
                    if (doctorId == _doctorId)
                    { 
                        Patient? patient = LoadPatientByPatientId(patientId);
                        if (patient != null && !patients.Contains(patient))
                        {
                            patients.Add(patient);
                        }
                    }
                }
            }
        }

        return patients;
    }
    
    public List<Patient> LoadWholePatients() // Load whole patients
    {
        List<Patient> patients = [];
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "textFiles", "users.txt");

        // Check if the file exists
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Error: The file 'users.txt' was not found at {filePath}");
        }
        
        string[] lines = File.ReadAllLines(filePath);
        for (int i = 1; i < lines.Length; i++)
        {
            var parts = lines[i].Split('/');
            string id = parts[0];
            string role = parts[2];
          
            switch (role)
            {
                case "Patient":
                    string name = parts[3];
                    string address = parts[4];
                    string email = parts[5];
                    string phone = parts[6];
                    patients.Add(new Patient(id, name, address, email, phone));
                    break;
            }
        }

        return patients;
    }
    
    public void AddPatient(string id, string password, string fullName, string email, string phone, string address)
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "textFiles", "users.txt");
        
        string directoryPath = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        
        string doctorLine = $"{id}/{password}/Patient/{fullName}/{address}/{email}/{phone}";
        
        try
        {
            if (!CheckIsIdExists(id))
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(doctorLine);
                }
                Console.WriteLine("Patient {0} added to the system!", fullName);
                LoginMenu loginMenu = new LoginMenu();
                loginMenu.LoadUsers();
            }
            else
            {
                Console.WriteLine("Id exists!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to the file: {ex.Message}");
        }
    }

    bool CheckIsIdExists(string patientId)
    {
        List<User> users = LoadWholeUsers();
        for (int i = 0; i < users.Count; i++)
        {
            if (users[i].Id == patientId)
            {
                return true;
            }
        }

        return false;
    }
    
    public List<User> LoadWholeUsers()
    {
        List<User> users = [];
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "textFiles", "users.txt");

        // Check if the file exists
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Error: The file 'users.txt' was not found at {filePath}");
        }
        
        string[] lines = File.ReadAllLines(filePath);
        for (int i = 1; i < lines.Length; i++)
        {
            var parts = lines[i].SplitAndValidate('/', 3); // use of extension method
            if (parts.Length == 0)
            {
                Console.WriteLine("Failed to Load user");
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

        return users;
    }
}