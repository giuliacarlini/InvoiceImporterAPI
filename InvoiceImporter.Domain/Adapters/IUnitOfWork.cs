namespace InvoiceImporter.Domain.Adapters
{
    public interface IUnitOfWork: IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}