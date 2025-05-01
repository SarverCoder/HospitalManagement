using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Dtos;

namespace HospitalManagement.Repository.Interfaces
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Task<DoctorWorkloadDto> GetDoctorWorkloadAsync(int doctorId, DateTime startDate, DateTime endDate);
        
        
    }
}
