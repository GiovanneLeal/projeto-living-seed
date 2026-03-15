using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LivingSeed.API.Data;
using LivingSeed.API.Models;
using LivingSeed.API.DTOs; 

namespace LivingSeed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjetosController(AppDbContext context)
        {
            _context = context;
        }

        // =======================================================
        // GET: Devolve a bandeja bonita (ProjetoRetornoDTO)
        // =======================================================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjetoRetornoDTO>>> GetProjetos()
        {
            var projetos = await _context.Projetos
                .Include(p => p.Usuario)
                .Include(p => p.Categoria)
                .Select(p => new ProjetoRetornoDTO 
                {
                    Id = p.Id,
                    Titulo = p.Titulo,
                    Descricao = p.Descricao,
                    MetaValor = p.MetaValor,
                    NomeEmpreendedor = p.Usuario.Nome,
                    NomeCategoria = p.Categoria.Nome
                })
                .ToListAsync();

            return Ok(projetos);
        }

        // =======================================================
        // POST: Recebe a bandeja enxuta (ProjetoCriacaoDTO)
        // =======================================================
        [HttpPost]
        public async Task<ActionResult> PostProjeto(ProjetoCriacaoDTO dto)
        {
            var novoProjeto = new Projeto
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                MetaValor = dto.MetaValor,
                UsuarioId = dto.UsuarioId,
                CategoriaId = dto.CategoriaId
            };

            // Salva no banco
            _context.Projetos.Add(novoProjeto);
            await _context.SaveChangesAsync();

            return Ok(new { Mensagem = "Projeto criado com sucesso!" });
        }
        // =======================================================
        // PUT (Atualizar um projeto existente)
        // Endereço na web ficará: PUT /api/projetos/1
        // =======================================================
        [HttpPut("{id}")] // O "{id}" avisa a API que um número vai vir na URL
        public async Task<IActionResult> PutProjeto(int id, ProjetoEdicaoDTO dto)
        {
            // 1. O cartório: Vamos no banco procurar a "Panela" (Model) original usando o ID
            var projeto = await _context.Projetos.FindAsync(id);

            // 2. Validação: E se o usuário tentou editar o projeto ID 999 que não existe?
            if (projeto == null)
            {
                return NotFound(new { Mensagem = "Projeto não encontrado." }); // Devolve Status 404
            }

            // 3. Atualizar os dados: Pegamos o que veio na "Bandeja" (dto) e jogamos na "Panela" (projeto)
            projeto.Titulo = dto.Titulo;
            projeto.Descricao = dto.Descricao;
            projeto.MetaValor = dto.MetaValor;
            projeto.CategoriaId = dto.CategoriaId;

            // 4. Salvar: O Entity Framework é inteligente. Ele percebeu que a variável 'projeto' 
            // foi modificada. Quando chamamos o SaveChanges, ele gera um 'UPDATE' no SQL só para esses campos.
            await _context.SaveChangesAsync();

            // 5. Resposta: O padrão HTTP para uma edição bem sucedida é 204 (No Content), 
            // que significa "Deu certo, mas não tenho nenhuma tela nova para te devolver".
            return NoContent();
        }
        // =======================================================
        // DELETE (Excluir um projeto)
        // Endereço na web ficará: DELETE /api/projetos/1
        // =======================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjeto(int id)
        {
            // 1. Procurar o projeto no banco
            var projeto = await _context.Projetos.FindAsync(id);

            if (projeto == null)
            {
                return NotFound(new { Mensagem = "Projeto não encontrado para exclusão." });
            }

            // 2. Dá a ordem de remoção na memória do Entity Framework
            _context.Projetos.Remove(projeto);

            // 3. Executa a remoção lá no SQL Server
            await _context.SaveChangesAsync();

            return NoContent(); // Status 204: Deletado com sucesso, nada mais a declarar.
        }
    }
}