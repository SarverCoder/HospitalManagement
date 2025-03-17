namespace HospitalManagement.DataAccess.Entities
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        public bool IsActive { get; set; }

        public DateTime AppointmentDate { get; set; }

        public int PatientId { get; set; }

        public Patient Patient { get; set; }

        public int DoctorId { get; set; }

        public Doctor Doctor { get; set; }
    }
}
