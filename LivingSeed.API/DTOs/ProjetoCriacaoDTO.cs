namespace LivingSeed.API.DTOs
{
    // Esta é a classe exata do que o usuário vai preencher no formulário da tela
    public class ProjetoCriacaoDTO
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal MetaValor { get; set; }

        // Em vez de mandar o objeto inteiro, mandamos só os números (IDs)
        public int UsuarioId { get; set; }
        public int CategoriaId { get; set; }
    }
}