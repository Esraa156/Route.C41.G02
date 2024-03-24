using Microsoft.EntityFrameworkCore;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.DAL.Data;
using Route.C41.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G02.BLL.Repositories
{
    public class GenericReposatory<T> : IGenericReposatory<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbcontext;


        public GenericReposatory(ApplicationDbContext applicationDbContext)
        {
            _dbcontext = applicationDbContext;
        }
        public int Add(T entity)
        {
            _dbcontext.Set<T>().Add(entity);
            return _dbcontext.SaveChanges();



        }



        public int Delete(T entity)
        {
            _dbcontext.Set<T>().Remove(entity);
            return _dbcontext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbcontext.Set<T>().AsNoTracking().ToList();


        }

        public T GetById(int id)
        {
            //var department= _dbcontext.Departments.Where(D=>D.Id==id).FirstOrDefault();
            //return department;

            var entity = _dbcontext.Set<T>().Find(id);
            return entity;


        }

        public int Update(T entity)
        {
            _dbcontext.Update(entity);
            return _dbcontext.SaveChanges();
        }

    
    }
    }
