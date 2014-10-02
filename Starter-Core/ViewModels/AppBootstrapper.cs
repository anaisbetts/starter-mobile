using System;
using ReactiveUI;
using Splat;
using Starter.Core.Views;
using Xamarin.Forms;
using ReactiveUI.XamForms;

namespace Starter.Core.ViewModels
{
    public class AppBootstrapper : ReactiveObject, IScreen
    {
        public RoutingState Router { get; protected set; }

        public AppBootstrapper()
        {
            Router = new RoutingState();
            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));

            // TODO: Register new views here, then navigate to the first page 
            // in your app
            Locator.CurrentMutable.Register(() => new TestView(), typeof(IViewFor<TestViewModel>));

            Router.Navigate.Execute(new TestViewModel(this));
        }

        public Page CreateMainPage()
        {
            return new RoutedViewHost();
        }
    }
}

