namespace DotnetAssignment1.models;

public class Appointment
{
    public string PatientId { get; set; }
    public string PatientName { get; set; }
    public string DoctorId { get; set; }
    public string DoctorName { get; set; }
    public string Description { get; set; }
    
    public Appointment(string patientName, string doctorName, string description)
    {
        PatientName = patientName;
        DoctorName = doctorName;
        Description = description;
    }
    
    public Appointment(string patientId, string patientName, string doctorId, string doctorName, string description)
    {
        PatientId = patientId;
        PatientName = patientName;
        DoctorId = doctorId;
        DoctorName = doctorName;
        Description = description;
    }
}