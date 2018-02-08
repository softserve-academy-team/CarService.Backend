using CarService.DbAccess.EF;
using Microsoft.EntityFrameworkCore;

namespace CarService.DbAccess.DAL
{ 
    public class SqlUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly DbContextOptionsBuilder<CarServiceDbContext> _optionsBuilder;
        
        public SqlUnitOfWorkFactory(DbContextOptionsBuilder<CarServiceDbContext> optionsBuilder)
        {
            _optionsBuilder = optionsBuilder;
        }

        public IUnitOfWork Create()
        {
            return new UnitOfWork(new CarServiceDbContext(_optionsBuilder.Options));
        }
    }
}