﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutor.Core.Entity;

namespace Tutor.Business.Repository
{
    public class SubjectRepository : BaseRepository
    {
        public IEnumerable<Subject> GetAll()
        {
            return _dbContext.Subjects.ToList();
        }

        public Subject GetSubject(int id)
        {
            return _dbContext.Subjects.FirstOrDefault(x => x.SubjectID == id);
        }
    }
}
