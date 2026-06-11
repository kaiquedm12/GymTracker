using FluentValidation;
using GymTrackerApi.DTOs.ExercicioDTOs;

namespace GymTrackerApi.Validators
{
    public class UpdateExercicioValidator : AbstractValidator<UpdateExercicioDTO>
    {
        public UpdateExercicioValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome do exercício é obrigatório.")
                .MaximumLength(100).WithMessage("O nome não pode ultrapassar 100 caracteres.");

            RuleFor(x => x.Repeticoes)
                .InclusiveBetween(1, 1000).WithMessage("As repetições devem estar entre 1 e 1000.");

            RuleFor(x => x.Series)
                .InclusiveBetween(1, 100).WithMessage("As séries devem estar entre 1 e 100.");

            RuleFor(x => x.Peso)
                .InclusiveBetween(0, 999.99m).WithMessage("O peso deve estar entre 0 e 999.99.");
        }
    }
}
