using AutoMapper;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Dtos;
using HospitalManagement.RedisCacheService;
using HospitalManagement.Repository.Interfaces;


namespace HospitalManagement.Services;


public interface IDoctorService
{
    Task<DoctorDto> GetDoctorByIdAsync(int doctorId);
    Task<DoctorWorkloadDto> GetDoctorWorkloadAsync(int doctorId, DateTime startDate, DateTime endDate);
}

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IMapper _mapper;
    private readonly IRedisCacheService _cacheService;
    private readonly TimeSpan CacheExpiration = TimeSpan.FromMinutes(10); // Cache expiration time

    public DoctorService(IDoctorRepository doctorRepository, IMapper mapper, IRedisCacheService cacheService)
    {
        _doctorRepository = doctorRepository;
        _mapper = mapper;
        _cacheService = cacheService;
    }


    public async Task<DoctorDto> GetDoctorByIdAsync(int doctorId)
    {
        var cacheKey = $"Doctor:{doctorId}";
        var cachedDoctor = await _cacheService.GetCacheValueAsync<Doctor>(cacheKey);
        if (cachedDoctor != null)
        {
            return _mapper.Map<DoctorDto>(cachedDoctor);
        }


        var doctor = await _doctorRepository.GetByIdAsync(doctorId);
        if (doctor != null)
        {
           await _cacheService.SetCacheValueAsync(cacheKey, doctor, CacheExpiration);
        }
        return _mapper.Map<DoctorDto>(doctor);

    }

    public async Task<DoctorWorkloadDto> GetDoctorWorkloadAsync(int doctorId, DateTime startDate, DateTime endDate)
    {
        return await _doctorRepository.GetDoctorWorkloadAsync(doctorId, startDate, endDate);
    }
}
