using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnachPat.Models
{
    public class AppDBContext : DbContext
    {

        public AppDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<PhotoModel> photos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }
}
