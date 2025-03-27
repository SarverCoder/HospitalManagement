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
        public PatientRepository(HospitalContext context) : base(context)
        {
            
        }
    }
}
