using Prism.Unity;
using Microsoft.Practices.Unity;
using System.Windows;

namespace PrismFun
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>(new ResolverOverride[] { });
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
