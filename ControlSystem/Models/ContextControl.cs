using System;
using System.Collections.Generic;
using System.Data.Entity;
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



        /** Steps to create the relation EF and generate tables
         * 1 - Package manage console and type: Enable-Migrations -ContextTypeName ContextControl -EnableAutomaticMigrations -Force
         * 2 - Go to Migrations Folder, configuration file, Add the line "AutomaticMigrationDataLossAllowed = true;"
         * 3 - Go to Global.asax and add the line "Database.SetInitializer(new MigrateDatabaseToLatestVersion<Models.ContextControl, Migrations.Configuration>());"
         * 
         * */
    }
}