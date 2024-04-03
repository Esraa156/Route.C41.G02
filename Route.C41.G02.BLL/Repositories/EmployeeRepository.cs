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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<Employee>GetEmployeeByAddress(string address)
        {
            return _dbContext.employees.Where(E => E.Address.Equals(address, StringComparison.OrdinalIgnoreCase));
        }

        public override async Task<IEnumerable<Employee>> GetAllAsync()
        => await _dbContext.Set<Employee>().Include(E => E.Department).AsNoTracking().ToListAsync();

        public IQueryable<Employee> SearchByName(string name)
            => _dbContext.employees.Where(E => E.Name.ToLower().Contains(name.ToLower())).Include(E => E.Department);

    }
}
    