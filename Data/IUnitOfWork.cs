﻿namespace ControleFinanceiroAPI.Data
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}