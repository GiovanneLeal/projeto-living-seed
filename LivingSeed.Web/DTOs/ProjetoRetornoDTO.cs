namespace LivingSeed.Web.DTOs
{
    public class ProjetoRetornoDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal MetaValor { get; set; }
        public string NomeEmpreendedor { get; set; } = string.Empty;
        public string NomeCategoria { get; set; } = string.Empty;
    }
}