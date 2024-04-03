﻿using Route.C41.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G02.BLL.Interfaces
{
    public interface IEmployeeRepository:IGenericReposatory<Employee>
    {
        IQueryable<Employee> GetEmployeeByAddress(string address);
        IQueryable<Employee> SearchByName(string name);
    }
}
