namespace LivingSeed.API.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty; // Depois vamos criptografar
        public string TipoUsuario { get; set; } = "Empreendedor"; // Ou "Investidor"
    }
}