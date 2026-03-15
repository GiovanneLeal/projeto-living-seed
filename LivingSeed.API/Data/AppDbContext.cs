// 1. Importações (Usings)
// Trazemos o "motor" do banco de dados e as nossas "plantas" (Models)
using LivingSeed.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace LivingSeed.API.Data
{
    // A nossa classe HERDA os superpoderes do DbContext padrão da Microsoft
    public class AppDbContext : DbContext
    {
        // 2. O Construtor
        // É aqui que a classe recebe aquele endereço (ConnectionString) que 
        // configuramos no Program.cs e repassa para a classe "mãe" (base)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // 3. As Tabelas (DbSets)
        // Cada DbSet abaixo representa uma tabela real que será criada no SQL Server.
        // O nome da propriedade (ex: Usuarios) será o nome da tabela no banco.

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<CategoriaSustentavel> Categorias { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Interesse> Interesses { get; set; }

        // 4. Bônus Profissional (OnModelCreating)
        // Este método é usado quando precisamos de regras muito específicas no banco
        // que as "Data Annotations" (como [Required]) não dão conta de fazer.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Regra do valor financeiro
            modelBuilder.Entity<Projeto>()
                .Property(p => p.MetaValor)
                .HasColumnType("decimal(18,2)");

            // 2. Regra da cascata dos Interesses
            modelBuilder.Entity<Interesse>()
                .HasOne(i => i.Investidor)
                .WithMany()
                .HasForeignKey(i => i.InvestidorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Interesse>()
                .HasOne(i => i.Projeto)
                .WithMany()
                .HasForeignKey(i => i.ProjetoId)
                .OnDelete(DeleteBehavior.Restrict);

            // =======================================================
            // 3. SEEDING (Semeando as Categorias Iniciais)
            // Usamos o 'HasData' para dizer ao banco o que ele deve inserir.
            // É obrigatório informar o 'Id' manualmente aqui!
            // =======================================================
            modelBuilder.Entity<CategoriaSustentavel>().HasData(
                new CategoriaSustentavel
                {
                    Id = 1,
                    Nome = "Energia Limpa",
                    Descricao = "Projetos focados em fontes renováveis como solar e eólica."
                },
                new CategoriaSustentavel
                {
                    Id = 2,
                    Nome = "Gestão de Resíduos",
                    Descricao = "Iniciativas de reciclagem e economia circular."
                },
                new CategoriaSustentavel
                {
                    Id = 3,
                    Nome = "Agricultura Sustentável",
                    Descricao = "Produção de alimentos com baixo impacto ambiental."
                },
                new CategoriaSustentavel
                {
                    Id = 4,
                    Nome = "Saneamento Básico",
                    Descricao = "Tecnologias para purificação e acesso à água limpa."
                }
            );
        }
    }
}