using Microsoft.Practices.Unity;
using Prism.Unity;
using ToDoApp.Views;
using System.Windows;

using Prism.Modularity;
using ToDoApp.Infrastructure;

namespace ToDoApp
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>(); 
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            var catalog = (ModuleCatalog)ModuleCatalog;
            catalog.AddModule(typeof(ModuleA_Module));
        }
    }
}
