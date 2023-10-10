namespace ControleFinanceiro.Domain.Entities
{
    public class Fatura
    {
        public Fatura(int idFatura, int idOrigem, DateTime vencimento, DateTime dataHoraCadastro, string nomeArquivo)
        {
            IdFatura = idFatura;
            IdOrigem = idOrigem;
            Vencimento = vencimento;
            DataHoraCadastro = dataHoraCadastro;
            NomeArquivo = nomeArquivo;
        }

        public int IdFatura { get; set; }
        public int IdOrigem { get; private set; }
        public DateTime Vencimento { get; private set; }
        public DateTime DataHoraCadastro { get; private set; }
        public string NomeArquivo { get; private set; } = string.Empty;
    }
}
