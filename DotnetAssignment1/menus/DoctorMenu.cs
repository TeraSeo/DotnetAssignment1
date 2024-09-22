using DotnetAssignment1.models;
using DotnetAssignment1.services;

namespace DotnetAssignment1.menus;

public class DoctorMenu
{
    private Doctor _doctor;
    private PatientService patientService = new PatientService();
    private AppointmentService appointmentService = new AppointmentService();
    
    public void ShowOptions(Doctor doctor)
    {
        _doctor = doctor;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=======================================");
            Console.WriteLine("   DOTNET Hospital Management System   ");
            Console.WriteLine("=======================================");
            Console.WriteLine("               Doctor Menu             ");
            Console.WriteLine();
            Console.WriteLine("1. List doctor details");
            Console.WriteLine("2. List patients");
            Console.WriteLine("3. List appointments");
            Console.WriteLine("4. Check particular patient");
            Console.WriteLine("5. List appointments with patient");
            Console.WriteLine("6. Logout");
            Console.WriteLine("7. Exit");
            Console.WriteLine();
            Console.Write("Please select an option (1-6): ");
            
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    // Navigate to the doctor details screen
                    ListDoctorDetails();
                    break;
                case "2":
                    // Navigate to the patient details screen
                    ListPatients();
                    break;
                case "3":
                    // Navigate to the appointments screen
                    ListAppointments();
                    break;
                case "4":
                    // Navigate to the book appointment screen
                    CheckParticularPatient();
                    break;
                case "5":
                    // Logout
                    ListAppointmentsWithPatient();
                    break;
                case "6":
                    // Logout
                    Logout();
                    return;
                case "7":
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

    private void ListDoctorDetails()
    {
        Console.Clear();
        Console.WriteLine("=======================================");
        Console.WriteLine("   DOTNET Hospital Management System   ");
        Console.WriteLine("=======================================");
        Console.WriteLine("               My Details              ");
        Console.WriteLine();
        Console.WriteLine("{0,-20} | {1,-25} | {2,-15} | {3,-30}", "Name", "Email Address", "Phone", "Address");
        Console.WriteLine(new string('-', 100));
        Console.WriteLine("{0,-20} | {1,-25} | {2,-15} | {3,-30}", _doctor.FullName, _doctor.Email, _doctor.Phone, _doctor.Address);
        Console.WriteLine();
    }
    
    private void ListPatients()
    {
        Console.Clear();
        Console.WriteLine("=======================================");
        Console.WriteLine("   DOTNET Hospital Management System   ");
        Console.WriteLine("=======================================");
        Console.WriteLine("              My Patients              ");
        Console.WriteLine();
        Console.WriteLine("{0,-20} | {1,-20} | {2,-30} | {3,-15} | {4,-30}", "Patient", "Doctor", "Email Address", "Phone", "Address");
        Console.WriteLine(new string('-', 140));
        List<Patient> patients = patientService.LoadPatientsByDoctorId(_doctor.Id);
        for (int i = 0; i < patients.Count; i++)
        {
            Console.WriteLine("{0,-20} | {1,-20} | {2,-30} | {3,-15} | {4,-30}", patients[i].FullName, _doctor.FullName, patients[i].Email, patients[i].Phone, patients[i].Address);
        }
        Console.WriteLine();
    }

    private void ListAppointments()
    {
        Console.Clear();
        Console.WriteLine("=======================================");
        Console.WriteLine("   DOTNET Hospital Management System   ");
        Console.WriteLine("=======================================");
        Console.WriteLine("            All Appointments           ");
        Console.WriteLine();
        Console.WriteLine("{0,-25} | {1,-25} | {2,-25}", "Doctor", "Patient", "Description");
        Console.WriteLine(new string('-', 100));
        List<Appointment> appointments = appointmentService.LoadAppointmentsByDoctorId(_doctor.Id);
        for (int i = 0; i < appointments.Count; i++)
        {
            Console.WriteLine("{0,-25} | {1,-25} | {2,-25}", appointments[i].DoctorName, appointments[i].PatientName, appointments[i].Description);
        }
        Console.WriteLine();
    }

    private void CheckParticularPatient()
    {
        Console.Clear();
        Console.WriteLine("=======================================");
        Console.WriteLine("   DOTNET Hospital Management System   ");
        Console.WriteLine("=======================================");
        Console.WriteLine("         Check Patient Details         ");
        Console.WriteLine();
        Console.Write("Enter the ID of the patient to check: ");
        string patientId = Console.ReadLine();
        Console.WriteLine();
        Console.WriteLine("{0,-20} | {1,-20} | {2,-30} | {3,-15} | {4,-30}", "Patient", "Doctor", "Email Address", "Phone", "Address");
        Console.WriteLine(new string('-', 140));
        Patient? patient = patientService.LoadPatientByPatientId(patientId);
        if (patient != null)
        {
            Console.WriteLine("{0,-20} | {1,-20} | {2,-30} | {3,-15} | {4,-30}", patient.FullName, _doctor.FullName, patient.Email, patient.Phone, patient.Address);
        }
        Console.WriteLine();
    }

    private void ListAppointmentsWithPatient()
    {
        Console.Clear();
        Console.WriteLine("=======================================");
        Console.WriteLine("   DOTNET Hospital Management System   ");
        Console.WriteLine("=======================================");
        Console.WriteLine("           Appointments With           ");
        Console.WriteLine();
        Console.Write("Enter the ID of the patient you would like to view appointments for: ");
        string patientId = Console.ReadLine();
        Console.WriteLine();
        Console.WriteLine("{0,-25} | {1,-25} | {2,-25}", "Doctor", "Patient", "Description");
        Console.WriteLine(new string('-', 100));
        List<Appointment> appointments = appointmentService.LoadAppointmentsByPatientIdNDoctorId(patientId, _doctor.Id);
        for (int i = 0; i < appointments.Count; i++)
        {
            Console.WriteLine("{0,-25} | {1,-25} | {2,-25}", appointments[i].DoctorName, appointments[i].PatientName, appointments[i].Description);
        }
        Console.WriteLine();
    }
    
    private void Logout()
    {
        LoginMenu loginMenu = new LoginMenu();
        loginMenu.Authenticate();
    }
}