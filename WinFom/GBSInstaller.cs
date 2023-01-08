using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace WinFom
{
    [RunInstaller(true)]
    public partial class GBSInstaller : System.Configuration.Install.Installer
    {
        public GBSInstaller()
        {
            InitializeComponent();
        }

        private void ProtectSection(string sectionName,
                     string provName, string exeFilePath)
        {
            System.Configuration.Configuration config =
              ConfigurationManager.OpenExeConfiguration(exeFilePath);
            ConfigurationSection section = config.GetSection(sectionName);

            if (!section.SectionInformation.IsProtected)
            {
                //Protecting the specified section with the specified provider
                section.SectionInformation.ProtectSection(provName);
            }
            section.SectionInformation.ForceSave = true;
            config.Save(ConfigurationSaveMode.Modified);

        }

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
            string sectionName = this.Context.Parameters["sectionName"];
            string provName = this.Context.Parameters["provName"];
            string exeFilePath = this.Context.Parameters["assemblypath"];

            ProtectSection(sectionName, provName, exeFilePath);
        }
    }
}
