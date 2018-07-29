using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MedbotService
{
    [RunInstaller(true)]
    public partial class Installer1 : Installer
    {
        // Constantes que armazenam uma nova chave no stateSaver contendo o path da instalação do Topshelf.
        private const string AssemblyIdentifier = "TopshelfAssembly";
        private const string InstallUtilAssemblyParameter = "assemblypath";

        public Installer1()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            var topshelfAssembly = Context.Parameters[InstallUtilAssemblyParameter];
            stateSaver.Add(AssemblyIdentifier, topshelfAssembly);

            RunHidden(topshelfAssembly, "install");

            base.Install(stateSaver);
        }

        public override void Uninstall(IDictionary savedState)
        {
            var topshelfAssembly = savedState[AssemblyIdentifier].ToString();

            RunHidden(topshelfAssembly, "uninstall");

            base.Uninstall(savedState);
        }

        /// <summary>
        /// Executa o processo de instalação ou desinstalação do Topshelf.
        /// </summary>
        /// <param name="primaryOutputAssembly">Path do assembly do Topshelf.</param>
        /// <param name="arguments">Comando a ser executado.</param>
        private static void RunHidden(string primaryOutputAssembly, string arguments)
        {
            var startInfo = new ProcessStartInfo(primaryOutputAssembly)
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = arguments,
                UseShellExecute = true
            };

            using (var process = Process.Start(startInfo))
            {
                process.WaitForExit();
            }
        }
    }
}
