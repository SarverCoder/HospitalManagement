using FluentValidation;
using HospitalManagement.DataAccess;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Dtos;
using HospitalManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HospitalManagement.Repository
{
    public class PatientRepository : Repository<Patient>, IPatientRepository 
    {
        private readonly IMemoryCache _cache;
        private readonly HospitalContext _context;

        public PatientRepository(HospitalContext context, IMemoryCache cache) : base(context)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            if (_cache.TryGetValue(id,out Patient? patient))
            {
                return patient;
            }

            var patientFromDb = await _context.Patients.FindAsync(id);
            if (patientFromDb != null)
            {
                _cache.Set(id, patientFromDb, TimeSpan.FromMinutes(10));
            }

            return patientFromDb;
        }
    }
}
