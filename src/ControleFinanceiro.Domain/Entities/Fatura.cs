using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Domain.Entities
{
    public class Fatura
    {
        public Fatura(int idOrigem, DateTime vencimento, string nomeArquivo)
        {
            IdFatura = new Guid();
            IdOrigem = idOrigem;
            Vencimento = vencimento;
            DataHoraCadastro = DateTime.Now;
            NomeArquivo = nomeArquivo;
        }

        public Guid IdFatura { get; private set; }

        [Required]
        public int IdOrigem { get; private set; }

        [Required]
        public DateTime Vencimento { get; private set; }

        [Required]
        public DateTime DataHoraCadastro { get; private set; }

        [MaxLength(50, ErrorMessage = "O nome do arquivo excede os 50 caracteres permitidos")]
        public string NomeArquivo { get; private set; } = string.Empty;

        public virtual List<Lancamento>? Lancamentos { get; set; }
    }
}
