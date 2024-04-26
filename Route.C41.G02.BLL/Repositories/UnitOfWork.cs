﻿using Microsoft.EntityFrameworkCore;
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
    public class UnitOfWork : IUnitOfWork, IAsyncDisposable
    {
        private readonly ApplicationDbContext _dbContext;

        //private Dictionary<string, IGenericRepository<ModelBase>> _repositories;

        private Hashtable _repositories;

        public UnitOfWork(ApplicationDbContext dbContext) // Ask CLR to create object from dbcontext class
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
        }

        public async Task< int> Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public async ValueTask DisposeAsync()
        {
           await _dbContext.DisposeAsync(); //closes the database conection
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
                    var repository = new GenericRepository<T>(_dbContext);
                    _repositories.Add(key, repository);
                }

            }

            return _repositories[key] as IGenericReposatory<T>;
        }
    }
}