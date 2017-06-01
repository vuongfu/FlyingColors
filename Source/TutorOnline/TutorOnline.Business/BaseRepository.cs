using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorOnline.DataAccess;

namespace TutorOnline.Business
{
    public class BaseRepository
    {
        public readonly TutorContext _dbContext = new TutorContext();
    }
}
