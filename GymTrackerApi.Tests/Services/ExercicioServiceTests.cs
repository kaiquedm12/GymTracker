using AutoMapper;
using GymTrackerApi.Data;
using GymTrackerApi.DTOs.ExercicioDTOs;
using GymTrackerApi.Models.Exercicios;
using GymTrackerApi.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace GymTrackerApi.Tests.Services
{
    public class ExercicioServiceTests
    {
        private readonly ExercicioService _service;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ExercicioServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateExercicioDTO, Exercicio>();
                cfg.CreateMap<Exercicio, ExercicioDTO>();
            });
            _mapper = config.CreateMapper();

            _service = new ExercicioService(_context, _mapper);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddExercicio()
        {
            var dto = new CreateExercicioDTO
            {
                Nome = "Supino",
                Repeticoes = 10,
                Series = 3,
                Peso = 80m
            };

            var result = await _service.CreateAsync(dto, "test-user");

            Assert.NotNull(result);
            Assert.Equal("Supino", result.Nome);
            Assert.Equal(10, result.Repeticoes);
            Assert.Equal(3, result.Series);
            Assert.Equal(80m, result.Peso);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAll()
        {
            _context.Exercicios.Add(new Exercicio { Nome = "Rosca", Repeticoes = 12, Series = 3, Peso = 20m, UserId = "u1" });
            _context.Exercicios.Add(new Exercicio { Nome = "Agachamento", Repeticoes = 8, Series = 4, Peso = 100m, UserId = "u1" });
            await _context.SaveChangesAsync();

            var result = await _service.GetAllAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
        {
            var result = await _service.GetByIdAsync(999);
            Assert.Null(result);
        }
    }
}
