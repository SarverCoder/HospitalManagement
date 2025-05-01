using AutoMapper;
using AutoMapper.QueryableExtensions;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Dtos;
using HospitalManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services;

public interface IPatientService 
{
    IList<PatientDto> GetAllPatients();
    Task<PatientDto> GetCachePatientById(int id);
    
}
public class PatientService(IPatientRepository _patientRepository, IMapper _mapper) : IPatientService
{
    public IList<PatientDto> GetAllPatients()
    {
        var patients = _patientRepository
            .GetAll()
            .AsNoTracking()
            .ProjectTo<PatientDto>(_mapper.ConfigurationProvider)
            .ToList();

        return patients;
    }

    public async Task<PatientDto> GetCachePatientById(int id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if (patient == null)
        {
            throw new KeyNotFoundException($"Patient with ID {id} not found.");
        }

        var patientDto = _mapper.Map<PatientDto>(patient);
        return patientDto;
    }
}
