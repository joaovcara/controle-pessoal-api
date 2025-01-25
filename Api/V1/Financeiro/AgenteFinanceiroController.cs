using Core.V1.Financeiro.AgenteFinanceiro.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.V1.Financeiro.AgenteFinanceiro.Models;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AgenteFinanceiroController : ControllerBase
    {
        private readonly IAgenteFinanceiroService _agenteFinanceiroService;

        public AgenteFinanceiroController(IAgenteFinanceiroService agenteFinanceiroService)
        {
            _agenteFinanceiroService = agenteFinanceiroService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(AgenteFinanceiroModel agenteFinanceiro)
        {
            await _agenteFinanceiroService.AddAsync(agenteFinanceiro);
            return Ok(new { message = "Agente Financeiro cadastrado com sucesso!" });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, AgenteFinanceiroModel agenteFinanceiro)
        {
            if (id == 0)
                return BadRequest("Informe um Id de AgenteFinanceiro válido.");

            await _agenteFinanceiroService.UpdateAsync(id, agenteFinanceiro);
            return Ok(new { message = "Agente Financeiro alterado com sucesso!" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _agenteFinanceiroService.DeleteAsync(id);
            return Ok(new { message = "Agente Financeiro deletado com sucesso!" });
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var agenteFinanceiros = await _agenteFinanceiroService.GetAllAsync();
            return Ok(agenteFinanceiros);
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var agenteFinanceiro = await _agenteFinanceiroService.GetByIdAsync(id);
            if (agenteFinanceiro == null)
                return NotFound("Agente Financeiro não encontrado.");

            return Ok(agenteFinanceiro);
        }
    }
}