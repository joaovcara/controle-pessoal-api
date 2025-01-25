using Core.V1.Cadastros.Categoria.Interfaces.Services;
using Core.V1.Cadastros.Categoria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CategoriaModel categoria)
        {
            var userIdClaim = User.FindFirst("Id");
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não autenticado.");
            }
            var usuarioId = int.Parse(userIdClaim.Value);

            categoria.UsuarioId = usuarioId;
            await _categoriaService.AddAsync(categoria);
            return Ok(new { message = "Categoria cadastrada com sucesso!" });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, CategoriaModel categoria)
        {
            if (id == 0)
                return BadRequest("Informe um Id de categoria válido.");

            var userIdClaim = User.FindFirst("Id");
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não autenticado.");
            }
            var usuarioId = int.Parse(userIdClaim.Value);

            categoria.UsuarioId = usuarioId;
            await _categoriaService.UpdateAsync(id, categoria);
            return Ok(new { message = "Categoria alterada com sucesso!" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userIdClaim = User.FindFirst("Id");
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não autenticado.");
            }
            var usuarioId = int.Parse(userIdClaim.Value);

            await _categoriaService.DeleteAsync(id, usuarioId);
            return Ok(new { message = "Categoria deletada com sucesso!" });
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var userIdClaim = User.FindFirst("Id");
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não autenticado.");
            }
            var usuarioId = int.Parse(userIdClaim.Value);

            var categorias = await _categoriaService.GetAllAsync(usuarioId);
            return Ok(categorias);
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userIdClaim = User.FindFirst("Id");
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não autenticado.");
            }
            var usuarioId = int.Parse(userIdClaim.Value);

            var categoria = await _categoriaService.GetByIdAsync(id, usuarioId);

            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        [HttpGet("getAllByTipoReceitaDespesa/{tipoReceitaDespesa}")]
        public async Task<IActionResult> GetAllByTipoReceitaDespesa(string tipoReceitaDespesa)
        {
            var userIdClaim = User.FindFirst("Id");
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não autenticado.");
            }
            var usuarioId = int.Parse(userIdClaim.Value);

            var categorias = await _categoriaService.GetAllByTipoReceitaDespesaAsync(tipoReceitaDespesa, usuarioId);
            return Ok(categorias);
        }
    }
}