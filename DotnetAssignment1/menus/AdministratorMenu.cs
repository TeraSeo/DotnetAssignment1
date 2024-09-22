using DotnetAssignment1.models;
using DotnetAssignment1.services;

namespace DotnetAssignment1.menus;

public class AdministratorMenu
{
    private DoctorService doctorService = new DoctorService();
    private PatientService patientService = new PatientService();
    private AppointmentService appointmentService = new AppointmentService();
    
    public void ShowOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=======================================");
            Console.WriteLine("   DOTNET Hospital Management System   ");
            Console.WriteLine("=======================================");
            Console.WriteLine("           Administrator Menu          ");
            Console.WriteLine();
            Console.WriteLine("1. List all doctors");
            Console.WriteLine("2. List doctor details");
            Console.WriteLine("3. List all patients");
            Console.WriteLine("4. Check patient details");
            Console.WriteLine("5. Add doctor");
            Console.WriteLine("6. Add patient");
            Console.WriteLine("7. Logout");
            Console.WriteLine("8. Exit");
            Console.WriteLine();
            Console.Write("Please select an option (1-8): ");
            
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    // Navigate to the doctor list screen
                    ListAllDoctors();
                    break;
                case "2":
                    // Navigate to the doctor details screen
                    ListDoctorDetails();
                    break;
                case "3":
                    // Navigate to the patients screen
                    ListAllPatients();
                    break;
                case "4":
                    // Navigate to the check patient screen
                    CheckPatientDetails();
                    break;
                case "5":
                    // Navigate to the add doctor screen
                    AddDoctor();
                    break;
                case "6":
                    // Navigate to the add patient screen
                    AddPatient();
                    break;
                case "7":
                    // Logout
                    Logout();
                    return;
                case "8":
                    // Exit the menu
                    Console.WriteLine("Exiting the patient menu...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please select a valid option.");
                    break;
            }
            
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }
    }
    
    private void ListAllDoctors()
    {
        Console.Clear();
        Console.WriteLine("=======================================");
        Console.WriteLine("   DOTNET Hospital Management System   ");
        Console.WriteLine("=======================================");
        Console.WriteLine("              All Doctors              ");
        Console.WriteLine();
        Console.WriteLine("All doctors registered to the DOTNET Hospital Management System");
        Console.WriteLine();
        Console.WriteLine("{0,-20} | {1,-25} | {2,-15} | {3,-30}", "Name", "Email Address", "Phone", "Address");
        Console.WriteLine(new string('-', 100));
        List<Doctor> doctors = doctorService.LoadWholeDoctors();
        for (int i = 0; i < doctors.Count; i++)
        {
            Console.WriteLine("{0,-20} | {1,-25} | {2,-15} | {3,-30}", doctors[i].FullName, doctors[i].Email, doctors[i].Phone, doctors[i].Address);
        }
        Console.WriteLine();
    }
    
    private void ListDoctorDetails()
    {
        Console.Clear();
        Console.WriteLine("=======================================");
        Console.WriteLine("   DOTNET Hospital Management System   ");
        Console.WriteLine("=======================================");
        Console.WriteLine("             Doctor Details            ");
        Console.WriteLine();
        Console.Write("Enter the ID of the doctor to check: ");
        string doctorId = Console.ReadLine();
        Console.WriteLine();
        Console.WriteLine("{0,-20} | {1,-25} | {2,-15} | {3,-30}", "Name", "Email Address", "Phone", "Address");
        Console.WriteLine(new string('-', 100));
        Doctor? doctor = doctorService.LoadDoctorByDoctorId(doctorId);
        if (doctor != null)
        {
            Console.WriteLine("{0,-20} | {1,-25} | {2,-15} | {3,-30}", doctor.FullName, doctor.Email, doctor.Phone, doctor.Address);
        }
        Console.WriteLine();
    }
    
    private void ListAllPatients()
    {
        Console.Clear();
        Console.WriteLine("=======================================");
        Console.WriteLine("   DOTNET Hospital Management System   ");
        Console.WriteLine("=======================================");
        Console.WriteLine("              All Patients             ");
        Console.WriteLine();
        Console.WriteLine("All patients registered to the DOTNET Hospital Management System");
        Console.WriteLine();
        Console.WriteLine("{0,-20} | {1,-20} | {2,-30} | {3,-15} | {4,-30}", "Patient", "Doctor", "Email Address", "Phone", "Address");
        Console.WriteLine(new string('-', 140));
        List<Patient> patients = patientService.LoadWholePatients();
        for (int i = 0; i < patients.Count; i++)
        {
            List<Appointment> appointments = appointmentService.LoadAppointmentsByPatientId(patients[i].Id);
            if (appointments.Count > 0)
            {
                Console.WriteLine("{0,-20} | {1,-20} | {2,-30} | {3,-15} | {4,-30}", patients[i].FullName, appointments[0].DoctorName, patients[i].Email, patients[i].Phone, patients[i].Address);
            }
            else
            {
                Console.WriteLine("{0,-20} | {1,-20} | {2,-30} | {3,-15} | {4,-30}", patients[i].FullName, "none", patients[i].Email, patients[i].Phone, patients[i].Address);
            }
        }
        Console.WriteLine();
    }
    
    private void CheckPatientDetails()
    {
        Console.Clear();
        Console.WriteLine("=======================================");
        Console.WriteLine("   DOTNET Hospital Management System   ");
        Console.WriteLine("=======================================");
        Console.WriteLine("             Patient Details           ");
        Console.WriteLine();
        Console.Write("Enter the ID of the patient to check: ");
        string patientId = Console.ReadLine();
        Console.WriteLine();
        Console.WriteLine("{0,-20} | {1,-20} | {2,-30} | {3,-15} | {4,-30}", "Patient", "Doctor", "Email Address", "Phone", "Address");
        Console.WriteLine(new string('-', 140));
        Patient? patient = patientService.LoadPatientByPatientId(patientId);
        if (patient != null)
        {
            List<Appointment> appointments = appointmentService.LoadAppointmentsByPatientId(patient.Id);
            if (appointments.Count > 0)
            {
                Console.WriteLine("{0,-20} | {1,-20} | {2,-30} | {3,-15} | {4,-30}", patient.FullName, appointments[0].DoctorName, patient.Email, patient.Phone, patient.Address);
            }
            else
            {
                Console.WriteLine("{0,-20} | {1,-20} | {2,-30} | {3,-15} | {4,-30}", patient.FullName, "none", patient.Email, patient.Phone, patient.Address);
            }
        }
        Console.WriteLine();
    }

    private void AddDoctor()
    {
        Console.Clear();
        Console.WriteLine("=======================================");
        Console.WriteLine("   DOTNET Hospital Management System   ");
        Console.WriteLine("=======================================");
        Console.WriteLine("               Add Doctor              ");
        Console.WriteLine();
        Console.WriteLine("Registering a new doctor with the DOTNET Hospital Management System");
        
        Console.Write("ID: ");
        string id = Console.ReadLine();
        
        Console.Write("Password: ");
        string password = Console.ReadLine();
        
        Console.Write("Full Name: ");
        string fullName = Console.ReadLine();
        
        Console.Write("Email: ");
        string email = Console.ReadLine();
        
        Console.Write("Phone: ");
        string phone = Console.ReadLine();
        
        Console.Write("Address: ");
        string address = Console.ReadLine();
        
        doctorService.AddDoctor(id, password, fullName, email, phone, address);
        Console.WriteLine();
    }
    
    private void AddPatient()
    {
        Console.Clear();
        Console.WriteLine("=======================================");
        Console.WriteLine("   DOTNET Hospital Management System   ");
        Console.WriteLine("=======================================");
        Console.WriteLine("               Add Patient             ");
        Console.WriteLine();
        Console.WriteLine("Registering a new patient with the DOTNET Hospital Management System");
        
        Console.Write("ID: ");
        string id = Console.ReadLine();
        
        Console.Write("Password: ");
        string password = Console.ReadLine();
        
        Console.Write("Full Name: ");
        string fullName = Console.ReadLine();
        
        Console.Write("Email: ");
        string email = Console.ReadLine();
        
        Console.Write("Phone: ");
        string phone = Console.ReadLine();
        
        Console.Write("Address: ");
        string address = Console.ReadLine();
        
        patientService.AddPatient(id, password, fullName, email, phone, address);
        Console.WriteLine();
    }
    
    private void Logout()
    {
        LoginMenu loginMenu = new LoginMenu();
        loginMenu.Authenticate();
    }
}