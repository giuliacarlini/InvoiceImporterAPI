﻿namespace ControleFinanceiro.Domain.Data
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}