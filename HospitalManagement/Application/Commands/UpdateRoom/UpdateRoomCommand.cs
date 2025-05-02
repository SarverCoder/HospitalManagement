using AutoMapper;
using HospitalManagement.Dtos;
using HospitalManagement.Repository.Interfaces;
using MediatR;

namespace HospitalManagement.Application.Commands.UpdateRoom;

public class UpdateRoomCommand(int id, UpdateRoomDto dto) : IRequest<RoomDto>
{
    public int Id { get; set; } = id;
    public UpdateRoomDto Dto { get; set; } = dto;

}

public class UpdateRoomCommandHandler(IRoomRepository repository,
    IMapper mapper) : IRequestHandler<UpdateRoomCommand, RoomDto>
{
    public async Task<RoomDto> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        var exitingRoom = await repository.GetByIdAsync(request.Id);

        if (exitingRoom == null)
        {
            return null;
        }

        mapper.Map(request.Dto, exitingRoom);
        await repository.SaveChangesAsync();

        return mapper.Map<RoomDto>(exitingRoom);

    }
}
