﻿using DE6ZVJ_ADT_2022_23_1.Modells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE6ZVJ_ADT_2022_23_1.Repository
{
    public interface IBookRepository : IRepository<Book>
    {
        void UpdateTitle(int id,string title);

    }
}
