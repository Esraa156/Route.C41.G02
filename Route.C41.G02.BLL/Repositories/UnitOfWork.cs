using Microsoft.EntityFrameworkCore;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections;
using Route.C41.G02.DAL.Models;
namespace Route.C41.G02.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private Hashtable _repositories;

        public UnitOfWork(ApplicationDbContext dbContext )

        {
            _dbContext = dbContext;
            // EmployeeRepository = new EmployeeRepository(_dbContext);
            // DepartmentRepository = new DepartmentRepository(_dbContext);
            _repositories = new Hashtable();


        }

        public IEmployeeRepository EmployeeRepository { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IDepartmentRepository DepartmentRepository { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //public IEmployeeRepository EmployeeRepository { get; set; }
        //public IDepartmentRepository DepartmentRepository { get; set; }
        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IGenericReposatory<T> Repository<T>() where T : ModelBase
        {
            var key = typeof(T).Name; //Employee

            if (!_repositories.ContainsKey(key))
            {
                if (key == nameof(Employee))
                {
                    var repository = new EmployeeRepository(_dbContext);
                    _repositories.Add(key, repository);
                }
                else
                {
                    var repository = new GenericReposatory<T>(_dbContext);
                    _repositories.Add(key, repository);
                }

            }

            return _repositories[key] as GenericReposatory<T>;
        }

        IGenericReposatory<T> IUnitOfWork.Repository<T>()
        {
            throw new NotImplementedException();
        }
    }
}

		
