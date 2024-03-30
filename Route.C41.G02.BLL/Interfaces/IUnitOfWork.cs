﻿using Route.C41.G02.BLL.Repositories;
using Route.C41.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G02.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable

    {
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }

        IGenericReposatory<T> Repository<T>() where T : ModelBase;

        int Complete();
    }
}
