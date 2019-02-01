using System;
using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Unity;
using PlagiarismDetectionApp.Views;
using PlagiarismDetectionApp.Models;

namespace PlagiarismDetectionApp
{
    public class ApplicationBootStrapper : UnityBootstrapper
    {
        private readonly Application app;

        public ApplicationBootStrapper(Application app)
        {
            this.app = app;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            app.MainWindow = (Window) Shell;
            app.MainWindow.Show();
            app.MainWindow.Activate();
        }

        protected override DependencyObject CreateShell()
        {
            return (Window) Container.Resolve(typeof(Shell));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            BootStrapPlagiarismComponent();
        }

        private void BootStrapPlagiarismComponent()
        {
            //Views
            Container.RegisterType<object, MainWindowView>("MainWindowView");

            //Others
            Container.RegisterType<MainWindowModel, MainWindowModel>(new ContainerControlledLifetimeManager());
        }
    }
}