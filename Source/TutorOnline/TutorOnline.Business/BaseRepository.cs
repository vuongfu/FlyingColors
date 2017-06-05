using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutor.DataAccess;


namespace TutorOnline.Business
{
    public class BaseRepository
    {
        public readonly TutorOnlineContext _dbContext = new TutorOnlineContext();
    }
}