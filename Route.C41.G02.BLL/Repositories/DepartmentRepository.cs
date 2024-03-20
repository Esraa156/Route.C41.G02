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
    internal class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbcontext;


        public DepartmentRepository(ApplicationDbContext applicationDbContext)
        {
            _dbcontext = applicationDbContext;
        }
        public int Add(Department department)
        {
            _dbcontext.Add(department);
           return _dbcontext.SaveChanges();



        }



        public int Delete(Department department)
        {
            _dbcontext.Departments.Remove(department);
            return _dbcontext.SaveChanges();
        }

        public IEnumerable<Department> GetAll()
        {
            return _dbcontext.Departments.AsNoTracking().ToList();


        }

        public Department GetById(int id)
        {
            //var department= _dbcontext.Departments.Where(D=>D.Id==id).FirstOrDefault();
            //return department;
        
            var department= _dbcontext.Departments.Find(id);
            return department;  


        }

        public int Update(Department department)
        {
            _dbcontext.Update(department);
            return _dbcontext.SaveChanges();
        }
    }
}
