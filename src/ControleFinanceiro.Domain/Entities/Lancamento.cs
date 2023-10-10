﻿namespace ControleFinanceiro.Domain.Entities
{
    public class Lancamento
    {
        public DateTime Data { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public bool? Parcelado { get; set; }
        public string Parcela { get; set; }
        public string TotalParcela { get; set; }
        public int IdImportacao { get; set; }
    }
}