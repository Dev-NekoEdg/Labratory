using Labratory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labratory.Infrastructure
{
    public class LaboratoryContext : DbContext
    {

        public LaboratoryContext(DbContextOptions<LaboratoryContext> options):base(options)
        {

        }

        public DbSet<Lists> Lists { get; set; }
        public DbSet<ListsItems> ListsItems { get; set; }

    }
}
