using DatapointAPIPOC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatapointAPIPOC.Db
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Analytics>()
            //    .Property(e => e.TimeAccessed)
            //    .HasDefaultValueSql("getdate()");
            //modelBuilder.Entity<ReportLinks>()
            //    .HasNoKey()
            //    .ToView("vw_ReportLinks");
            //modelBuilder.Entity<ReportResourceLinks>()
            //    .Property(e => e.ID)
            //    .HasDefaultValueSql("newid()");
            //modelBuilder.Entity<CompanyMapping>()
            //    .HasNoKey();
        }

        //public DbSet<Analytics> Analytics { get; set; }
        //public DbSet<ReportSections> ReportSections { get; set; }
        //public DbSet<ReportRegions> ReportRegions { get; set; }
        //public DbSet<ReportResourceLinks> ReportResourceLinks { get; set; }
        //public DbSet<DomainServers> DomainServers { get; set; }
        //public DbSet<ReportResourceMapping> ReportResourceMapping { get; set; }
        //public DbSet<BusinessFunction> BusinessFunction { get; set; }
        //public DbSet<BusinessRole> BusinessRole { get; set; }

        //public DbSet<ReportLinks> ReportLinks { get; set; }
        //public DbSet<ReportCategories> ReportCategories { get; set; }
        //public DbSet<AdminUsers> AdminUsers { get; set; }
        //public DbSet<AppConfig> AppConfig { get; set; }
        //public DbSet<CompanyMapping> CompanyMapping { get; set; }


        //public DbSet<DashboardLinkedElements> DashboardLinkedElements { get; set; }
        //public DbSet<DashboardsInfo> DashboardsInfo { get; set; }
        public DbSet<WidgetStructure> WidgetStructure { get; set; }
        //public DbSet<BusinessRoleDashboardMapping> BusinessRoleDashboardMapping { get; set; }
        //public DbSet<Region> Region { get; set; }

    }
}
