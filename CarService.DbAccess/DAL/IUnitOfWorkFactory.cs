namespace CarService.DbAccess.DAL
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
