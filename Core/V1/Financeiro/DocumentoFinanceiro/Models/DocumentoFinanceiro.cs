using System.ComponentModel.DataAnnotations.Schema;

namespace Core.V1.Financeiro.DocumentoFinanceiro.Models
{
    [Table("DocumentoFinanceiro")]
    public class DocumentoFinanceiroModel
    {
        /// <summary>
        /// Obtém ou define o id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtém ou define a data de cadastro
        /// </summary>
        public DateTime DataDocumento { get; set; }

        /// <summary>
        /// Obtém ou define o número do documento
        /// </summary>
        public string NumeroDocumento { get; set; } = string.Empty;

        /// <summary>
        /// Obtém ou define a descrição
        /// </summary>
        public string Descricao { get; set; } = string.Empty;

        /// <summary>
        /// Obtém ou define o Id da Categoria
        /// </summary>
        public int IdCategoria { get; set; }

        /// <summary>
        /// Obtém ou define a descrição da categoria
        /// </summary>
        public string? DescricaoCategoria { get; set; } = string.Empty;

        /// <summary>
        /// Obtém ou define a cor da categoria
        /// </summary>
        public string? CorCategoria { get; set; } = string.Empty;

        /// <summary>
        /// Obtém ou define o icone da categoria
        /// </summary>
        public string? IconeCategoria { get; set; } = string.Empty;

        /// <summary>
        /// Obtém ou define o valor
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Obtém ou define a data de vencimento
        /// </summary>
        public DateTime DataVencimento { get; set; }

        /// <summary>
        /// Obtém ou define a data de pagamento
        /// </summary>
        public DateTime? DataPagamento { get; set; }

        /// <summary>
        /// Obtém ou define o Id do usuário
        /// </summary>
        public int UsuarioId { get; set; } 
    }
}
