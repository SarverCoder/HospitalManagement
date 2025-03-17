namespace HospitalManagement.DataAccess.Entities
{
    public class Patient
    {
        public Patient()
        {
            Appointments = new List<Appointment>();
        }

        public int PatientId { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public bool IsActive { get; set; }

        public DateTime RegisteredDate { get; set; }

        public int? PatientBlankId { get; set; }

        public PatientBlank? PatientBlank { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

    }
}
