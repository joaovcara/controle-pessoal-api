using Core.V1.Financeiro.DocumentoFinanceiro.Models;
using Core.V1.Financeiro.DocumentoFinanceiro.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentoFinanceiroController : ControllerBase
    {
        private readonly IDocumentoFinanceiroService _documentoFinanceiroService;

        public DocumentoFinanceiroController(IDocumentoFinanceiroService documentoFinanceiro)
        {
            _documentoFinanceiroService = documentoFinanceiro;
        }

        /// <summary>
        /// Adiciona um registro de documento financeiro
        /// </summary>
        /// <param name="documentoFinanceiro">Instância da classe DocumentoFinanceiroModel</param>
        /// <returns>Sucesso ou erro</returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create(DocumentoFinanceiroModel documentoFinanceiro)
        {
            var userIdClaim = User.FindFirst("Id");
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não autenticado.");
            }
            var usuarioId = int.Parse(userIdClaim.Value); 

            documentoFinanceiro.UsuarioId = usuarioId;
            await _documentoFinanceiroService.AddAsync(documentoFinanceiro);
            return Ok(new { message = "Documento Financeiro cadastrado com sucesso!" });
        }

        /// <summary>
        /// Altera um registro de documento financeiro
        /// </summary>
        /// <param name="id">Id do documento financeiro a ser alterado</param>
        /// <param name="documentoFinanceiro">Instância da classe DocumentoFinanceiroModel</param>
        /// <returns>Sucesso ou erro</returns>
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, DocumentoFinanceiroModel documentoFinanceiro)
        {
            if (id == 0)
                return BadRequest("Informe um Id de documento financeiro válido.");

            var userIdClaim = User.FindFirst("Id");
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não autenticado.");
            }
            var usuarioId = int.Parse(userIdClaim.Value);
            
            documentoFinanceiro.UsuarioId = usuarioId;
            await _documentoFinanceiroService.UpdateAsync(id, documentoFinanceiro);
            return Ok(new { message = "Documento Financeiro alterado com sucesso!" });
        }

        /// <summary>
        /// Deleta um registro de documento financeiro
        /// </summary>
        /// <param name="id">Id do documento financeiro a ser apagado</param>
        /// <returns>Sucesso ou erro</returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _documentoFinanceiroService.DeleteAsync(id);
            return Ok(new { message = "Documento Financeiro deletado com sucesso!" });
        }

        /// <summary>
        /// Consulta todos os registros de documentos financeiros
        /// </summary>
        /// <returns>Sucesso ou erro</returns>
        [HttpGet("getAll/{tipoReceitaDespesa}")]
        public async Task<IActionResult> GetAll(string tipoReceitaDespesa)
        {
            var userIdClaim = User.FindFirst("Id");
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não autenticado.");
            }
            var usuarioId = int.Parse(userIdClaim.Value);

            var documentosFinanceiros = await _documentoFinanceiroService.GetAllAsync(usuarioId, tipoReceitaDespesa);
            return Ok(documentosFinanceiros);
        }

        /// <summary>
        /// Consulta um documento financeiro pelo Id
        /// </summary>
        /// <param name="id">Id do documento financeiro a ser consultado</param>
        /// <returns>Sucesso ou erro</returns>
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userIdClaim = User.FindFirst("Id");
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não autenticado.");
            }
            var usuarioId = int.Parse(userIdClaim.Value);
            
            var documentoFinanceiro = await _documentoFinanceiroService.GetByIdAsync(id, usuarioId);

            if (documentoFinanceiro == null)
                return NotFound();

            return Ok(documentoFinanceiro);
        }

        /// <summary>
        /// Pagamento de documento financeiro
        /// </summary>
        /// <param name="id">Id do documento</param>
        /// <param name="documentoFinanceiro">Data do pagamento</param>
        /// <returns>Sucesso ou erro</returns>
        [HttpPost("pagamento/{id}")]
        public async Task<IActionResult> Pagamento(int id, [FromBody] DocumentoFinanceiroModel documentoFinanceiro)
        {
            var userIdClaim = User.FindFirst("Id");
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não autenticado.");
            }
            var usuarioId = int.Parse(userIdClaim.Value);
            
            documentoFinanceiro.UsuarioId = usuarioId;
            await _documentoFinanceiroService.PagamentoAsync(id, documentoFinanceiro);
            return Ok(new { message = "Pagamento efetuado com sucesso!" });
        }
    }
}
