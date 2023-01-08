using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFom.Admin.Database;
using WinFom.Admin.Forms;

using WinFom.Common.Model;
using WinFom.Migrations;
using WinFom.ReadyStuff.Forms;

namespace WinFom
{
    static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Configuration>());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(SingleTon.LoginForm);
            //Application.Run(new AboutUsForm());

            //Application.Run(new Test.Form1());
        }
    }
}
