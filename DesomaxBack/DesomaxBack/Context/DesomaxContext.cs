using DesomaxBack.Models;
using Microsoft.EntityFrameworkCore;

namespace DesomaxBack.Context
{
    public class DesomaxContext : DbContext
    {
        public DesomaxContext(DbContextOptions<DesomaxContext> options) : base(options)
        {

        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
