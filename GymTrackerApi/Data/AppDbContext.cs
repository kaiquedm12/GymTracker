using Microsoft.EntityFrameworkCore;
using GymTrackerApi.Models;
using GymTrackerApi.Models.Treinos;
using GymTrackerApi.Models.Exercicios;
using GymTrackerApi.Models.Relacionamentos;
using GymTrackerApi.Models.Pessoas;

namespace GymTrackerApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Treino> Treinos { get; set; }
        public DbSet<Exercicio> Exercicios { get; set; }
        public DbSet<TreinoExercicio> TreinosExercicios { get; set; }
        public DbSet<Personal> Personais { get; set; }
        public DbSet<Aluno> Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TreinoExercicio>()
                .HasKey(te => new { te.TreinoId, te.ExercicioId });

            modelBuilder.Entity<TreinoExercicio>()
                .HasOne(te => te.Treino)
                .WithMany(t => t.TreinoExercicio)
                .HasForeignKey(te => te.TreinoId);

            modelBuilder.Entity<TreinoExercicio>()
                .HasOne(te => te.Exercicio)
                .WithMany(e => e.TreinosExercicios)
                .HasForeignKey(te => te.ExercicioId);

            modelBuilder.Entity<Aluno>()
                .HasOne(a => a.Personal)
                .WithMany(p => p.Alunos)
                .HasForeignKey(a => a.PersonalId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Treino>()
                .HasOne(t => t.Aluno)
                .WithMany(a => a.Treinos)
                .HasForeignKey(t => t.AlunoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Exercicio>()
                .HasOne(e => e.Aluno)
                .WithMany(a => a.Exercicios)
                .HasForeignKey(e => e.AlunoId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
