
using LegalContractApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace LegalContractApi.Data
{
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<LegalContract> LegalContracts => Set<LegalContract>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
        }
    }
}
