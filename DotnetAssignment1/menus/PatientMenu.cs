using DotnetAssignment1.models;
using DotnetAssignment1.services;

namespace DotnetAssignment1.menus;

public class PatientMenu
{
    private Patient _patient;
    private PatientService patientService = new PatientService();
    private DoctorService doctorService = new DoctorService();
    private AppointmentService appointmentService = new AppointmentService();
    
    public void ShowOptions(Patient patient)
    {
        _patient = patient;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=======================================");
            Console.WriteLine("   DOTNET Hospital Management System   ");
            Console.WriteLine("=======================================");
            Console.WriteLine("              Patient Menu             ");
            Console.WriteLine();
            Console.WriteLine("Welcome to DOTNET Hospital Management System {0}", patient.FullName);
            Console.WriteLine();
            Console.WriteLine("1. List patient details");
            Console.WriteLine("2. List my doctor details");
            Console.WriteLine("3. List all appointments");
            Console.WriteLine("4. Book appointment");
            Console.WriteLine("5. Exit to login");
            Console.WriteLine("6. Exit System");
            Console.WriteLine();
            Console.Write("Please select an option (1-6): ");
            
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    // Navigate to the patient details screen
                    ListPatientDetails();
                    break;
                case "2":
                    // Navigate to the doctor details screen
                    ListDoctorDetails();
                    break;
                case "3":
                    // Navigate to the appointments screen
                    ListAllAppointments();
                    break;
                case "4":
                    // Navigate to the book appointment screen
                    BookAppointment();
                    break;
                case "5":
                    // Exit to login
                    ExitToLogin();
                    return;
                case "6":
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
    
    private void ListPatientDetails()
    {
        Console.Clear();
        Console.WriteLine("=======================================");
        Console.WriteLine("   DOTNET Hospital Management System   ");
        Console.WriteLine("=======================================");
        Console.WriteLine("               My Details              ");
        Console.WriteLine();
        Console.WriteLine("{0}'s Details", _patient.FullName);
        Console.WriteLine();
        Console.WriteLine(_patient.ToString());
    }

    private void ListDoctorDetails()
    {
        Console.Clear();
        Console.WriteLine("=======================================");
        Console.WriteLine("   DOTNET Hospital Management System   ");
        Console.WriteLine("=======================================");
        Console.WriteLine("               My Doctor               ");
        Console.WriteLine();
        Console.WriteLine("Your doctor:");
        Console.WriteLine();
        string doctorId = doctorService.LoadDoctorIdByPatientId(_patient.Id); // Load patient's doctor ids 
        Doctor? doctor = doctorService.LoadDoctorByDoctorId(doctorId); // Load doctor details by doctor id
        if (doctor != null)
        {
            Console.WriteLine("{0,-20} | {1,-25} | {2,-15} | {3,-30}", "Name", "Email Address", "Phone", "Address");
            Console.WriteLine(new string('-', 100));
            Console.WriteLine("{0,-20} | {1,-25} | {2,-15} | {3,-30}", doctor.FullName, doctor.Email, doctor.Phone, doctor.Address);
            Console.WriteLine();
        }
    }

    private void ListAllAppointments()
    {
        Console.Clear();
        Console.WriteLine("=======================================");
        Console.WriteLine("   DOTNET Hospital Management System   ");
        Console.WriteLine("=======================================");
        Console.WriteLine("            My Appointments            ");
        Console.WriteLine();
        Console.WriteLine("Appointments for {0}", _patient.FullName);
        Console.WriteLine();
        Console.WriteLine("{0,-20} | {1,-25} | {2,-30}", "Doctor", "Patient", "Description");
        Console.WriteLine(new string('-', 100));
        List<Appointment> appointments = appointmentService.LoadAppointmentsByPatientId(_patient.Id);
        for (int i = 0; i < appointments.Count; i++)
        {
            Appointment appointment = appointments[i];
            Console.WriteLine("{0,-20} | {1,-25} | {2,-30}", appointment.DoctorName, appointment.PatientName, appointment.Description);
        }
        Console.WriteLine();
    }
    
    private void BookAppointment()
    {
        Console.Clear();
        Console.WriteLine("=======================================");
        Console.WriteLine("   DOTNET Hospital Management System   ");
        Console.WriteLine("=======================================");
        Console.WriteLine("            Book Appointment           ");
        Console.WriteLine();
        
        List<Appointment> appointments = appointmentService.LoadAppointmentsByPatientId(_patient.Id);

        int doctorIndex;
        String description = "";
        if (appointments.Count == 0) // if patient haven't made appointment
        {
            Console.WriteLine("You are not registered with any doctor! Please choose which doctor you would like to register with");
            List<Doctor> doctors = doctorService.LoadWholeDoctors();
            
            for (int i = 0; i < doctors.Count; i++)
            {
                Console.WriteLine("{0,-3} {1,-20} | {2,-25} | {3,-15} | {4,-30}", i + 1, doctors[i].FullName, doctors[i].Email, doctors[i].Phone, doctors[i].Address);
            }
            
            bool isValidInput = false;

            // Loop until valid input is entered
            while (!isValidInput)
            {
                Console.Write("Please choose a doctor: ");
                string doctorNumber = Console.ReadLine();

                if (int.TryParse(doctorNumber, out doctorIndex) && doctorIndex >= 1 && doctorIndex <= doctors.Count)
                {
                    isValidInput = true; 
                    Console.WriteLine("You are booking a new appointment with {0}", doctors[doctorIndex - 1].FullName);
                    Console.Write("Description of the appointment: ");
                    description = Console.ReadLine();
                    if (description != "")
                    {
                        BookAppointment(doctors[doctorIndex - 1], _patient.Id, description);
                    }
                    else
                    {
                        while (description == "")
                        {
                            Console.Write("Please type description of the appointment: ");
                            description = Console.ReadLine();
                        }

                        BookAppointment(doctors[doctorIndex - 1], _patient.Id, description);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid doctor number. Please select a number between 1 and {0}.", doctors.Count);
                }
            }
        }
        else  // if patient have made appointment
        {
            Doctor doctor = doctorService.LoadDoctorByDoctorId(appointments[0].DoctorId);
            if (doctor != null)
            {
                Console.WriteLine("You are booking a new appointment with {0}", appointments[0].DoctorName);
                Console.Write("Description of the appointment: ");
                description = Console.ReadLine();
                if (description != "")
                {
                    BookAppointment(doctor, _patient.Id, description);
                }
                else
                {
                    while (description == "")
                    {
                        Console.Write("Please type description of the appointment: ");
                        description = Console.ReadLine();
                    }

                    BookAppointment(doctor, _patient.Id, description);
                }   
            }
        }
    }

    private void ExitToLogin()
    {
        LoginMenu loginMenu = new LoginMenu();
        loginMenu.Authenticate();
    }
    
    private void BookAppointment(Doctor doctor, string patientId, string description) // use of overloading
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "textFiles", "appointments.txt");

        string directoryPath = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        Patient patient = patientService.LoadPatientByPatientId(patientId);
        string appointmentLine = $"{patient.Id}/{patient.FullName}/{doctor.Id}/{doctor.FullName}/{description}";
        
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(appointmentLine);
            }
            Console.WriteLine("The appointment has been booked successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to the file: {ex.Message}");
        }
    }
}