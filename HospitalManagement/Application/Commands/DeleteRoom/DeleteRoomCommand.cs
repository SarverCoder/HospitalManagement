using HospitalManagement.Repository.Interfaces;
using MediatR;

namespace HospitalManagement.Application.Commands.DeleteRoom;

public record DeleteRoomCommand(int Id) : IRequest<bool>;


public class DeleteRoomCommandHandler(IRoomRepository repository) : IRequestHandler<DeleteRoomCommand, bool>
{
    public async Task<bool> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await repository.GetByIdAsync(request.Id);
        if (room == null)
        {
            return false;
        }
        repository.Delete(room);
        await repository.SaveChangesAsync();

        return true;
    }
}
