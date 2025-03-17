using HospitalManagement.DataAccess.Entities;

namespace HospitalManagement.Dtos
{
    public class ArrangeAppointmentDto
    {

        public bool IsActive { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }

        public Appointment ToAppointment()
        {
           return new Appointment()
            {
                IsActive = this.IsActive,
                PatientId = this.PatientId,
                AppointmentDate = this.AppointmentDate,
                DoctorId = this.DoctorId
            };
        }
    }
}
