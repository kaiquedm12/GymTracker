using Microsoft.EntityFrameworkCore;
using GymTrackerApi.Models;
using GymTrackerApi.Models.Treinos;
using GymTrackerApi.Models.Exercicios;
using GymTrackerApi.Models.Relacionamentos;

namespace GymTrackerApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Tabelas principais
        public DbSet<Treino> Treinos { get; set; }
        public DbSet<Exercicio> Exercicios { get; set; }

        // Tabela de relação
        public DbSet<TreinoExercicio> TreinosExercicios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura a chave composta da tabela intermediária
            modelBuilder.Entity<TreinoExercicio>()
                .HasKey(te => new { te.TreinoId, te.ExercicioId });

            // Relacionamento Treino -> TreinoExercicio
            modelBuilder.Entity<TreinoExercicio>()
                .HasOne(te => te.Treino)
                .WithMany(t => t.TreinoExercicio)
                .HasForeignKey(te => te.TreinoId);

            // Relacionamento Exercicio -> TreinoExercicio
            modelBuilder.Entity<TreinoExercicio>()
                .HasOne(te => te.Exercicio)
                .WithMany(e => e.TreinosExercicios)
                .HasForeignKey(te => te.ExercicioId);
        }
    }
}
