using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartUpApp.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartUpApp.DataAccess.Maps
{
    public class CategoryMapping
    {
        public CategoryMapping(EntityTypeBuilder<Category> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Id).ValueGeneratedOnAdd();
            entityBuilder.Property(x => x.Name).HasMaxLength(500);           
        }
    }
}
