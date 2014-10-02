using System;
using System.Collections.Generic;
using Xamarin.Forms;
using ReactiveUI;
using Starter.Core.ViewModels;

namespace Starter.Core.Views
{    
    public partial class TestView : ContentPage, IViewFor<TestViewModel>
    {    
        public TestView ()
        {
            InitializeComponent ();

            this.OneWayBind(ViewModel, x => x.TheGuid, x => x.theGuid.Text);
        }

        public TestViewModel ViewModel {
            get { return (TestViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create<TestView, TestViewModel>(x => x.ViewModel, default(TestViewModel), BindingMode.OneWay);

        object IViewFor.ViewModel {
            get { return ViewModel; }
            set { ViewModel = (TestViewModel)value; }
        }
    }
}

