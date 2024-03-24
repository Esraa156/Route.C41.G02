﻿using Route.C41.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G02.BLL.Interfaces
{
    public interface IGenericReposatory<T> where T : ModelBase
    {
        IEnumerable<T> GetAll();

        T GetById(int id);


        int Add(T entity);

        int Update(T entity);


        int Delete(T entity);

    }
}