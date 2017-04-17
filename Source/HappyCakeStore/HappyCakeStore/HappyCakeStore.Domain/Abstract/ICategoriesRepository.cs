﻿using HappyCakeStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyCakeStore.Domain.Abstract
{
    public interface ICategoriesRepository
    {
        IEnumerable<Category> Categories { get; }
    }
}
