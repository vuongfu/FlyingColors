using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartUpApp.Core
{
    public class EntityBase
    {
        public int Id { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsNewEntity
        {
            get { return Id == 0; }
        }
    }
}
