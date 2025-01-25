namespace Core.V1.Financeiro.AgenteFinanceiro.Models
{
    public class AgenteFinanceiroModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int TipoAgenteFinanceiroId { get; set; }
        public int? BancoId { get; set; }
        public int? Agencia { get; set; }
        public int? DigitoAgencia { get; set; }
        public int? Conta { get; set; }
        public int? DigitoConta { get; set; }
        public bool ComputaSaldo { get; set; }
    }
}