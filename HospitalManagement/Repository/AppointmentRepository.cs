using HospitalManagement.DataAccess;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Repository
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        private readonly HospitalContext _context;
        public AppointmentRepository(HospitalContext context) : base(context)
        {
            _context = context;
        }

        public async Task<string> SheduleAppointment(int doctorId, int patientId, DateTime appointmentDate)
        {
            var doctor = await _context.Doctors
                .Include(d => d.Speciality)
                .FirstOrDefaultAsync(d => d.DoctorId == doctorId && d.IsActive);

            if (doctor == null)
            {
                return "Doctor not found";
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.PatientId == patientId && p.IsActive);

            if (patient == null)
            {
                return "Doctor is busy this time";
            }

            bool isDoctorBusy = await _context.Appointments
                .AnyAsync(a => a.PatientId == patientId && a.AppointmentDate == appointmentDate && a.IsActive);

            if (isDoctorBusy)
            {
                return "The patient has another appointment at this time.";
            }

            var appointment = new Appointment
            {
                DoctorId = doctorId,
                PatientId = patientId,
                AppointmentDate = appointmentDate,
                IsActive = true
            };

            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();

            return "The meeting was successfully planned.";
        }
    }
}
