using DotnetAssignment1.models;

namespace DotnetAssignment1.services;

public class AppointmentService
{
    private PatientService patientService = new PatientService();
    public List<Appointment> LoadAppointmentsByPatientId(string patientId) // Load appointments by patient id
    {
        List<Appointment> appointments = [];
        
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "textFiles", "appointments.txt");

        // Check if the file exists
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Error: The file 'appointments.txt' was not found at {filePath}");
        }
        
        string[] lines = File.ReadAllLines(filePath);
        for (int i = 1; i < lines.Length; i++)
        {
            if (lines[i] != "" && lines[i].Contains("/"))
            {
                var parts = lines[i].Split('/');
                if (parts.Length > 4)
                {
                    string _patientId = parts[0];
                    string patientName = parts[1];
                    string doctorId = parts[2];
                    string doctorName = parts[3];
                    string description = parts[4];

                    if (patientId == _patientId)
                    {
                        Appointment appointment = new Appointment(_patientId, patientName, doctorId, doctorName, description);
                        appointments.Add(appointment);
                    }
                }
            }
        }
        return appointments;
    }
    
    public List<Appointment> LoadAppointmentsByDoctorId(string doctorId) // Load appointments by doctor id
    {
        List<Appointment> appointments = [];
        
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "textFiles", "appointments.txt");

        // Check if the file exists
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Error: The file 'appointments.txt' was not found at {filePath}");
        }
        
        string[] lines = File.ReadAllLines(filePath);
        for (int i = 1; i < lines.Length; i++)
        {
            if (lines[i] != "" && lines[i].Contains("/"))
            {
                var parts = lines[i].Split('/');
                if (parts.Length > 4)
                {
                    string _patientId = parts[0];
                    string patientName = parts[1];
                    string _doctorId = parts[2];
                    string doctorName = parts[3];
                    string description = parts[4];

                    if (doctorId == _doctorId)
                    {
                        Appointment appointment = new Appointment(_patientId, patientName, doctorId, doctorName, description);
                        appointments.Add(appointment);
                    }
                }
            }
        }
        return appointments;
    }
    
    public List<Appointment> LoadAppointmentsByPatientIdNDoctorId(string patientId, string doctorId) // Load appointments by patient id and doctor id
    {
        List<Appointment> appointments = [];
        
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "textFiles", "appointments.txt");

        // Check if the file exists
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Error: The file 'appointments.txt' was not found at {filePath}");
        }
        
        string[] lines = File.ReadAllLines(filePath);
        for (int i = 1; i < lines.Length; i++)
        {
            if (lines[i] != "" && lines[i].Contains("/"))
            {
                var parts = lines[i].Split('/');
                if (parts.Length > 4)
                {
                    string _patientId = parts[0];
                    string patientName = parts[1];
                    string _doctorId = parts[2];
                    string doctorName = parts[3];
                    string description = parts[4];

                    if (patientId == _patientId && doctorId == _doctorId)
                    {
                        Appointment appointment = new Appointment(_patientId, patientName, doctorId, doctorName, description);
                        appointments.Add(appointment);
                    }
                }
            }
        }
        return appointments;
    }
}