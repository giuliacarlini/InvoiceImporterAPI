namespace ControleFinanceiroAPI.Model
{
    public class Importacao
    {
        public int IdImportacao { get; set; }
        public string date { get; set; } 
        public string category { get; set; }
        public string title { get; set; }
        public string amount { get; set; }
        public DateTime DataHoraImportacao { get; set; }
        public int IdOrigemImportacao { get; set; }
        public int IdFatura { get; set; }

    }
}
