using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route.C41.G02.DAL.Models;

namespace Route.C41.G02.BLL.Interfaces
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();

        Department GetById(int id);


        int Add(Department department);

        int Update(Department department);


        int Delete(Department department);

    }
}
