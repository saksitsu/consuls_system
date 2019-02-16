using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CONSULS_SYSTEM.Models;

namespace CONSULS_SYSTEM.DATA
{
    public class CONSULS_Context:DbContext
    {
        CONSULS_Context() { }
        public CONSULS_Context(DbContextOptions<CONSULS_Context> options) : base(options)
        {
        }

        public DbSet<CONSULS_SYSTEM.Models.TB_News> TB_News { get; set; }
        public DbSet<CONSULS_SYSTEM.Models.TB_Log> TB_Log { get; set; }
        public DbSet<CONSULS_SYSTEM.Models.TB_Member> TB_Member { get; set; }
        public DbSet<CONSULS_SYSTEM.Models.MT_Level> MT_Level { get; set; }
        public DbSet<CONSULS_SYSTEM.Models.MT_Country> MT_Country { get; set; }
        public DbSet<CONSULS_SYSTEM.Models.TB_Menu_Information> TB_Menu_Information { get; set; }
        public DbSet<CONSULS_SYSTEM.Models.TB_Menu_Authorize> TB_Menu_Authorize { get; set; }
        public DbSet<CONSULS_SYSTEM.Models.TB_Meeting> TB_Meeting { get; set; }
        public DbSet<CONSULS_SYSTEM.Models.TB_Events> TB_Events { get; set; }
        public DbSet<CONSULS_SYSTEM.Models.MT_AboutUS> MT_AboutUS { get; set; }
        public DbSet<CONSULS_SYSTEM.Models.MT_ContactUS> MT_ContactUS { get; set; }
        public DbSet<CONSULS_SYSTEM.Models.TB_MessageToAdmin> TB_MessageToAdmin { get; set; }
        public DbSet<CONSULS_SYSTEM.Models.MT_Partner> MT_Partner { get; set; }
        public DbSet<CONSULS_SYSTEM.Models.MT_Home> MT_Home { get; set; }
        public DbSet<CONSULS_SYSTEM.Models.TB_Webboard> TB_Webboard { get; set; }
        public DbSet<CONSULS_SYSTEM.Models.TB_Minutes> TB_Minutes { get; set; }
        public DbSet<CONSULS_SYSTEM.Models.TB_Comment> TB_Comment { get; set; }
        public DbSet<CONSULS_SYSTEM.Models.TB_Privilege> TB_Privilege { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<TB_Events>()
            //    .Property(b => b.Level_ID)
            //    .HasColumnName("level");

            modelBuilder.Entity<TB_Webboard>()
            .Ignore(x => x.image_user)
            .Ignore(y => y.name_user);

            modelBuilder.Entity<TB_Comment>()
            .Ignore(x => x.image_user)
            .Ignore(y => y.name_user);

            modelBuilder.Entity<TB_MessageToAdmin>()
                .Ignore(x => x.Country_ID);
        }
    }
}
