using System.Text;
using System.Text.Json;
using HospitalManagement.DataAccess;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Dtos;
using HospitalManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;



namespace HospitalManagement.Repository
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private readonly HospitalContext _context;
        private readonly IMemoryCache _cache;
        

        public DoctorRepository(HospitalContext context, IMemoryCache cache) : base(context)
        {
            _context = context;
            _cache = cache;
            
        }



        


        public async Task<DoctorWorkloadDto> GetDoctorWorkloadAsync(int doctorId, DateTime startDate, DateTime endDate)
        {
            var query = await _context.Appointments
                .Where(a => a.DoctorId == doctorId &&
                            a.AppointmentDate >= startDate &&
                            a.AppointmentDate <= endDate &&
                            a.IsActive)
                .Include(a => a.Patient)
                .ToListAsync();

            if (!query.Any())
            {
                return new DoctorWorkloadDto
                {
                    TotalAppointments = 0,
                    AverageDuration = 0,
                    PatientNames = new List<string>()
                };
            }

            int totalAppointments = query.Count;

            double averageDuration = query.Average(a => a.Duration.Minutes);

            List<string> patientNames = query
                .Select(a => $"{a.Patient.Firstname} {a.Patient.Lastname}")
                .Distinct()
                .ToList();

            return new DoctorWorkloadDto
            {
                TotalAppointments = totalAppointments,
                AverageDuration = averageDuration,
                PatientNames = patientNames
            };


        }
    }
}
