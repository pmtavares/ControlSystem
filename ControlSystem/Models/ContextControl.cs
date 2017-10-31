using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ControlSystem.Models
{
    public class ContextControl : DbContext
    {

        public ContextControl() : base("DefaultConnection")
        {

        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>(); // Remove the cascade references that is set by standard for circular reference
        }


        public System.Data.Entity.DbSet<ControlSystem.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<ControlSystem.Models.Groups> Groups { get; set; }

        public System.Data.Entity.DbSet<ControlSystem.Models.GroupsDetails> GroupsDetails { get; set; }

        public System.Data.Entity.DbSet<ControlSystem.Models.Scores> Scores { get; set; }



        /** Steps to create the relation EF and generate tables
         * 1 - Package manage console and type: Enable-Migrations -ContextTypeName ContextControl -EnableAutomaticMigrations -Force
         * 2 - Go to Migrations Folder, configuration file, Add the line "AutomaticMigrationDataLossAllowed = true;"
         * 3 - Go to Global.asax and add the line "Database.SetInitializer(new MigrateDatabaseToLatestVersion<Models.ContextControl, Migrations.Configuration>());"
         * 
         * */
    }
}