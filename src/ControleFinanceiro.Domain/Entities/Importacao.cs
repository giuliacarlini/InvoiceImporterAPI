namespace ControleFinanceiro.Domain.Entities
{
    public class Importacao
    {
        public Importacao(string data, string categoria, string descricao, string valor, int idOrigemImportacao, int idFatura)
        {
            Data = data;
            Categoria = categoria;
            Descricao = descricao;
            Valor = valor;
            DataHoraImportacao = DateTime.Now;
            IdOrigemImportacao = idOrigemImportacao;
            IdFatura = idFatura;
        }

        public int IdImportacao { get; set; }
        public string Data { get; private set; } = string.Empty;
        public string Categoria { get; private set; } = string.Empty;
        public string Descricao { get; private set; } = string.Empty;
        public string Valor { get; private set; } = string.Empty;
        public DateTime DataHoraImportacao { get; private set; }
        public int IdOrigemImportacao { get; private set; }
        public int IdFatura { get; private set; }
    }
}
