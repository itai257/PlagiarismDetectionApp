using System;
using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Unity;
using PlagiarismDetectionApp.Views;
using PlagiarismDetectionApp.Models;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using PlagiarismDetectionApp.ViewModels;

namespace PlagiarismDetectionApp
{
    public class ApplicationBootStrapper : UnityBootstrapper
    {
        private readonly Application app;
        private readonly IRegionManager regionManager;

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
            Container.RegisterType<AboutView>("AboutView");
            Container.RegisterType<MainWindowView>("MainWindowView");
            Container.RegisterType<ResultsWindowView>("ResultsWindowView");
            Container.RegisterType<AlgorithmView>("AlgorithmView");
            Container.RegisterTypeForNavigation<ResultsWindowView>();
            Container.RegisterTypeForNavigation<AboutView>();
            Container.RegisterTypeForNavigation<AlgorithmView>();

            //Others
            Container.RegisterType<MainWindowModel>(new ContainerControlledLifetimeManager());
            Container.RegisterType<AlgorithmViewModel>();

        }
    }
}