using HospitalManagement.DataAccess.Entities;

namespace HospitalManagement.Repository.Interfaces;

public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<string> SheduleAppointment(int doctorId, int patientId, DateTime appointmentDate);
}
