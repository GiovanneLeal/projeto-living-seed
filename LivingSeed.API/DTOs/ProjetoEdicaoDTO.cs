namespace LivingSeed.API.DTOs
{
    // Bandeja estrita: só permitimos alterar o que faz sentido para o negócio.
    // Note que não tem UsuarioId nem Id do projeto aqui dentro.
    public class ProjetoEdicaoDTO
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal MetaValor { get; set; }
        public int CategoriaId { get; set; } // Ele pode mudar a categoria se tiver classificado errado
    }
}