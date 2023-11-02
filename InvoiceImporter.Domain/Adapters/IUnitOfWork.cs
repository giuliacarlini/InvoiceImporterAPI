namespace InvoiceImporter.Domain.Adapters
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}