using GymTrackerApi.DTOs.ExercicioDTOs;
using GymTrackerApi.Validators;

namespace GymTrackerApi.Tests.Validators
{
    public class CreateExercicioValidatorTests
    {
        private readonly CreateExercicioValidator _validator;

        public CreateExercicioValidatorTests()
        {
            _validator = new CreateExercicioValidator();
        }

        [Fact]
        public void Should_Pass_When_Valid()
        {
            var dto = new CreateExercicioDTO
            {
                Nome = "Supino Reto",
                Repeticoes = 10,
                Series = 3,
                Peso = 80m
            };

            var result = _validator.Validate(dto);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Should_Fail_When_Nome_Empty()
        {
            var dto = new CreateExercicioDTO
            {
                Nome = "",
                Repeticoes = 10,
                Series = 3,
                Peso = 80m
            };

            var result = _validator.Validate(dto);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1001)]
        public void Should_Fail_When_Repeticoes_OutOfRange(int reps)
        {
            var dto = new CreateExercicioDTO
            {
                Nome = "Teste",
                Repeticoes = reps,
                Series = 3,
                Peso = 80m
            };

            var result = _validator.Validate(dto);
            Assert.False(result.IsValid);
        }
    }
}
