using CarService.DbAccess.EF;

namespace CarService.DbAccess.DAL
{ 
    public class SqlUnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return new UnitOfWork(new CarServiceDbContext());
        }
    }
}