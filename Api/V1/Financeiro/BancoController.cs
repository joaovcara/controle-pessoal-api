using Core.V1.Financeiro.Banco.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.V1.Financeiro.Banco.Models;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BancoController : ControllerBase
    {
        private readonly IBancoService _bancoService;

        public BancoController(IBancoService bancoService)
        {
            _bancoService = bancoService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(BancoModel banco)
        {
            await _bancoService.AddAsync(banco);
            return Ok(new { message = "Banco cadastrado com sucesso!" });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, BancoModel banco)
        {
            if (id == 0)
                return BadRequest("Informe um Id de banco válido.");

            await _bancoService.UpdateAsync(id, banco);
            return Ok(new { message = "Banco alterado com sucesso!" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bancoService.DeleteAsync(id);
            return Ok(new { message = "Banco deletado com sucesso!" });
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var bancos = await _bancoService.GetAllAsync();
            return Ok(bancos);
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var banco = await _bancoService.GetByIdAsync(id);
            if (banco == null)
                return NotFound("Banco não encontrado.");

            return Ok(banco);
        }
    }
}