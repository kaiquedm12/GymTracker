using FluentValidation;
using GymTrackerApi.DTOs.PersonalDTOs;

namespace GymTrackerApi.Validators
{
    public class CreatePersonalValidator : AbstractValidator<CreatePersonalDTO>
    {
        public CreatePersonalValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(200).WithMessage("O nome não pode ultrapassar 200 caracteres.");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("E-mail inválido.")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.Telefone)
                .MaximumLength(20).WithMessage("O telefone não pode ultrapassar 20 caracteres.");
        }
    }
}
