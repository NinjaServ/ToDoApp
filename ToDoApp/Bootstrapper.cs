using Microsoft.Practices.Unity;
using Prism.Unity;
using ToDoApp.Views;
using System.Windows;

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
    }
}
