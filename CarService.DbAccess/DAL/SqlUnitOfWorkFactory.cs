using System;
using CarService.DbAccess.EF;
using Microsoft.EntityFrameworkCore;

namespace CarService.DbAccess.DAL
{ 
    public class SqlUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly Action<DbContextOptionsBuilder> _sqlServerOptionsAction;
        
        public SqlUnitOfWorkFactory(Action<DbContextOptionsBuilder> sqlServerOptionsAction)
        {
            _sqlServerOptionsAction = sqlServerOptionsAction;
        }

        public IUnitOfWork Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            _sqlServerOptionsAction?.Invoke(optionsBuilder);
            return new UnitOfWork(new CarServiceDbContext(optionsBuilder.Options));
        }
    }
}

