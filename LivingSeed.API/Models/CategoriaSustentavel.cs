namespace LivingSeed.API.Models
{
    public class CategoriaSustentavel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty; // Ex: Energia Solar
        public string Descricao { get; set; } = string.Empty;
    }
}