using FluentValidation;
using GymTrackerApi.DTOs.TreinoDTOs;

namespace GymTrackerApi.Validators
{
    public class UpdateTreinoValidator : AbstractValidator<UpdateTreinoDTO>
    {
        public UpdateTreinoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome do treino é obrigatório.")
                .MaximumLength(200).WithMessage("O nome não pode ultrapassar 200 caracteres.");

            RuleFor(x => x.Data)
                .NotEmpty().WithMessage("A data do treino é obrigatória.");

            RuleFor(x => x.DuracaoMinutos)
                .InclusiveBetween(1, 1440).WithMessage("A duração deve estar entre 1 minuto e 24 horas.");

            RuleFor(x => x.AlunoId)
                .GreaterThan(0).WithMessage("O ID do aluno é obrigatório.");
        }
    }
}
