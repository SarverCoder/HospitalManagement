using AutoMapper;
using HospitalManagement.Dtos;
using HospitalManagement.Repository.Interfaces;
using MediatR;

namespace HospitalManagement.Application.Queries.GetDoctors;

public class GetDoctorsQuery : IRequest<IEnumerable<DoctorDto>>
{

}

public class GetDoctorsQueryHandler(IDoctorRepository _doctorRepository, IMapper _mapper) : IRequestHandler<GetDoctorsQuery, IEnumerable<DoctorDto>>
{
    public async Task<IEnumerable<DoctorDto>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
    {
        var doctors = await _doctorRepository.GetAllAsync();

        var doctorDtos = _mapper.Map<IEnumerable<DoctorDto>>(doctors);

        return doctorDtos;
    }
}
