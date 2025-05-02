using AutoMapper;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Dtos;
using HospitalManagement.Repository.Interfaces;
using MediatR;

namespace HospitalManagement.Application.Commands.CreateRoom;

public class CreateRoomCommand(CreateRoomDto dto) : IRequest<int>
{
    public CreateRoomDto Dto { get; set; } = dto;
}

public class CreateRoomCommandHandler(
    IRoomRepository roomRepository, IMapper mapper) : IRequestHandler<CreateRoomCommand, int>
{
    public async Task<int> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var room = mapper.Map<Room>(request.Dto);
        await roomRepository.AddAsync(room);
        await roomRepository.SaveChangesAsync();

        return room.Id;

    }
}
