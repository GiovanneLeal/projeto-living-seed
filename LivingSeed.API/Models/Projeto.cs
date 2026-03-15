using System.ComponentModel.DataAnnotations; // Necessário para o [Key]
using System.ComponentModel.DataAnnotations.Schema; // <-- Adicione esta linha!

namespace LivingSeed.API.Models
{
    public class Projeto
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal MetaValor { get; set; }

        // Relacionamentos (Chaves Estrangeiras)
        public int UsuarioId { get; set; } // O Dono do projeto
        public Usuario? Usuario { get; set; }

        public int CategoriaId { get; set; }
        public CategoriaSustentavel? Categoria { get; set; }
    }
}