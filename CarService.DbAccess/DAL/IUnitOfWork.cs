using System;
using System.Threading.Tasks;
using CarService.DbAccess.Entities;

namespace CarService.DbAccess.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class, IEntity;
        void Save();
        Task SaveAsync();
    }
}