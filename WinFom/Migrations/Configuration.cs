namespace WinFom.Migrations
{
    using Admin.Database;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Runtime.Remoting.Contexts;

    internal sealed class Configuration : DbMigrationsConfiguration<Admin.Database.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Admin.Database.Context context)
        {
            DbSeed.Seed(context);
        }
    }
}
