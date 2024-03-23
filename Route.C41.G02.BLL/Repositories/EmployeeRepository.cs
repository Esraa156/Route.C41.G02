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
    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public EmployeeRepository(ApplicationDbContext applicationDbContext)
        {
            _dbcontext = applicationDbContext;
        }
        public int Add(Employee employee)
        {
            _dbcontext.employees.Add(employee);
            return _dbcontext.SaveChanges();



        }



        public int Delete(Employee employee)
        {
            _dbcontext.employees.Remove(employee);
            return _dbcontext.SaveChanges();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _dbcontext.employees.AsNoTracking().ToList();


        }

        public Employee GetById(int id)
        {
            //var department= _dbcontext.Departments.Where(D=>D.Id==id).FirstOrDefault();
            //return department;

            var employee = _dbcontext.employees.Find(id);
            return employee;
        }

        public int Update(Employee employee)
        {
            _dbcontext.Update(employee);
            return _dbcontext.SaveChanges();
        }
    }
}
