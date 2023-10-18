namespace ControleFinanceiro.Domain.Entities
{
    public class LancamentoImportacao
    {
        public LancamentoImportacao(string linha)
        {
            IdImportacao = Guid.NewGuid();
            Linha = linha;
        }

        public Guid IdImportacao { get; set; }
        public string Linha { get; private set; } = string.Empty;
        public int IdFatura { get; private set; }
    }
}
