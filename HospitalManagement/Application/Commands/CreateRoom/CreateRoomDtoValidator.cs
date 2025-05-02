using FluentValidation;
using HospitalManagement.Dtos;

namespace HospitalManagement.Application.Commands.CreateRoom
{
    public class CreateRoomDtoValidator: AbstractValidator<CreateRoomDto>
    {
        public CreateRoomDtoValidator()
        {
            RuleFor(r => r.RoomNumber)
                .NotNull()
                .MaximumLength(3)
                .WithMessage("Must be 3 characters");

            RuleFor(r => r.Capacity)
                .NotNull()
                .WithMessage("Capacity is required.")
                .GreaterThan(0)
                .WithMessage("Capacity must be greater than 0.")
                .LessThan(100)
                .WithMessage("Capacity must be less than 100.");
        }
    }
}
