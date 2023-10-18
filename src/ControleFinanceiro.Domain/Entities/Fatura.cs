using ControleFinanceiro.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Domain.Entities
{
    public class Fatura
    {
        public Fatura(TipoImportacao tipoImportacao, DateTime vencimento, string nomeArquivo)
        {
            IdFatura = Guid.NewGuid();
            IdOrigem = (int)tipoImportacao;
            Vencimento = vencimento;
            DataHoraCadastro = DateTime.Now;
            NomeArquivo = nomeArquivo;
            Lancamentos = new List<Lancamento>();
        }

        public Guid IdFatura { get; private set; }

        [Required]
        public int IdOrigem { get; private set; }

        [Required]
        public DateTime Vencimento { get; private set; }

        [Required]
        public DateTime DataHoraCadastro { get; private set; }

        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        public string NomeArquivo { get; private set; }

        public List<Lancamento> Lancamentos { get; set; }
    }
}
