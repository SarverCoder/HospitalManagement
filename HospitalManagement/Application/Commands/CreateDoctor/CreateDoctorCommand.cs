using AutoMapper;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Dtos;
using HospitalManagement.Repository.Interfaces;
using MediatR;

namespace HospitalManagement.Application.Commands.CreateDoctor;

public class CreateDoctorCommand(CreateDoctorDto dto) : IRequest<int>
{
    public CreateDoctorDto Dto { get; set; } = dto;
}

public class CreateDoctorCommandHandler(IDoctorRepository _doctorRepository
    , IMapper mapper) : IRequestHandler<CreateDoctorCommand, int>
{
    public async Task<int> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
       var doctor = mapper.Map<Doctor>(request.Dto);

        await _doctorRepository.AddAsync(doctor);
        await _doctorRepository.SaveChangesAsync();

        return doctor.DoctorId;

    }
}
