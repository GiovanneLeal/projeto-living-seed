namespace LivingSeed.API.DTOs
{
    // Esta é a classe do que vamos mostrar na "Vitrine" de projetos
    public class ProjetoRetornoDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal MetaValor { get; set; }

        // Em vez de devolver o ID ou o Usuário com a senha, 
        // devolvemos apenas os nomes em texto para a tela exibir!
        public string NomeEmpreendedor { get; set; } = string.Empty;
        public string NomeCategoria { get; set; } = string.Empty;
    }
}