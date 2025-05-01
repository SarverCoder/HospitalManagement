namespace HospitalManagement.Dtos
{
    public class DoctorWorkloadDto
    {
        public int TotalAppointments { get; set; }
        public double AverageDuration { get; set; }
        public List<string> PatientNames { get; set; }
    }
}
