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
    public class DepartmentRepository : GenericReposatory<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext DbContext)
        : base(DbContext) { }


    }
}
