using Prism.Unity;
using Microsoft.Practices.Unity;
using System.Windows;
using PrismFun.Views;
using PrismFun.Models;

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

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<IDummyService, DummyService>();

            Container.RegisterTypeForNavigation<TelephoneBookView>();
            Container.RegisterTypeForNavigation<LastTelephoneBookEntryView>();
        }
    }
}
