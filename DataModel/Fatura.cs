namespace ControleFinanceiroAPI.Model
{
    public class Fatura
    {
        public int IdFatura { get; set; }
        public int IdOrigem { get; set; }
        public DateTime Vencimento { get; set; }
        public DateTime DataHoraCadastro { get; set; }
    }
}
