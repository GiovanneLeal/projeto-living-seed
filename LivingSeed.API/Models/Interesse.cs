using System;

namespace LivingSeed.API.Models
{
    public class Interesse
    {
        public int Id { get; set; }
        public DateTime DataInteresse { get; set; } = DateTime.Now;

        // Quem se interessou?
        public int InvestidorId { get; set; }
        public Usuario? Investidor { get; set; }

        // Por qual projeto?
        public int ProjetoId { get; set; }
        public Projeto? Projeto { get; set; }
    }
}