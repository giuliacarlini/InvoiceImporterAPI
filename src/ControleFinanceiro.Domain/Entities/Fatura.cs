namespace ControleFinanceiro.Domain.Entities
{
    public class Fatura
    {
        public int IdFatura { get; set; }
        public int IdOrigem { get; set; }
        public DateTime Vencimento { get; set; }
        public DateTime DataHoraCadastro { get; set; }
        public string NomeArquivo { get; set; } = string.Empty;
        public virtual List<Lancamento> Lancamentos { get; set; }
    }
}
