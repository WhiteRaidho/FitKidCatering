using Microsoft.EntityFrameworkCore;

namespace FitKidCateringApp.Models
{
    public class ApplicationDbContext : DbContext
    {
        #region OnModelCreating()
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        #endregion

        #region ApplicationDbContext()
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        #endregion
    }
}
