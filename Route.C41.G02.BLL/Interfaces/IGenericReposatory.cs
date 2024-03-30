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


        void Add(T entity);

        void Update(T entity);


        void Delete(T entity);

    }
}