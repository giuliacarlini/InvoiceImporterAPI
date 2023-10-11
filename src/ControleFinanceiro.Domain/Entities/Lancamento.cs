namespace ControleFinanceiro.Domain.Entities
{
    public class Lancamento
    {
        public DateTime Data { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public bool? Parcelado { get; set; }
        public string Parcela { get; set; } = string.Empty;
        public string TotalParcela { get; set; } = string.Empty;
        public int IdImportacao { get; set; }
        public virtual LancamentoImportacao? LancamentoImportacao { get; set; }
    }
}
