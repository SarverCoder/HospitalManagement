using FluentValidation;
using HospitalManagement.Dtos;

namespace HospitalManagement.Application.Commands.CreateDoctor;

public class CreateDoctorDtoValidator : AbstractValidator<CreateDoctorDto>
{
    public CreateDoctorDtoValidator()
    {
        RuleFor(x => x.Firstname)
            .NotEmpty()
            .WithMessage("Firstname is required.")
            .MaximumLength(10);

        RuleFor(x => x.Lastname).NotEmpty().WithMessage("Lastname is required.")
            .MaximumLength(100);

        RuleFor(x => x.IsActive)
            .NotNull()
            .WithMessage("IsActive is required.");


    }
}

