using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutor.DataAccess;

namespace Tutor.Business
{
    public class BaseRepository
    {
        public readonly TutorContext _dbContext = new TutorContext();
    }
}
