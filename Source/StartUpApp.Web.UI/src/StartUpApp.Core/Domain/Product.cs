using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartUpApp.Core.Domain
{
    public class Product : EntityBase
    {
        public string Name { get; set; }

        /// <summary>
        /// Relational fields
        /// </summary>
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
