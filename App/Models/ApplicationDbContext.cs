using Microsoft.EntityFrameworkCore;
﻿using FitKidCateringApp.Models.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitKidCateringApp.Models
{
    public class ApplicationDbContext : IdentityDbContext<CoreUser, CoreRole, long>
    {
        #region OnModelCreating()
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        #endregion

        #region ApplicationDbContext()
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        #endregion

        public DbSet<CoreUser> CoreUsers { get; set; }
        public DbSet<CoreRole> CoreRoles { get; set; }
    }
}
