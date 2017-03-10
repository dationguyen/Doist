using Prism.Unity.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Practices.Unity;
using ToDoist.Services;

namespace ToDoist
{

    public enum Pages { Main, Sheet, Floor, Room }

    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : PrismUnityApplication
    {
        public App()
        {
            InitializeComponent();

            this.Suspending += OnSuspending;
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity    

            deferral.Complete();
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            NavigationService.Navigate(Pages.Main.ToString(), null);
            return Task.FromResult(true);
        }
        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            Container.RegisterInstance(this.NavigationService);
            Container.RegisterType<IToDoistApiClient, ToDoistApiClient>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IDataService, DataService>(new ContainerControlledLifetimeManager());
            return base.OnInitializeAsync(args);
        }


    }
}
