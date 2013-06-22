using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ReactiveUI.Android;
using ReactiveUI;
using Starter.Core.ViewModels;

namespace Starter.Views
{
    [Activity (Label = "Starter-Android", MainLauncher = true)]
    public class TestActivity : ReactiveActivity, IViewFor<TestViewModel>
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);
            
            button.Click += delegate
            {
                button.Text = string.Format("{0} clicks!", count++);
            };

            TheGuid = FindViewById<TextView>(Resource.Id.TheGuid);

            ViewModel = new TestViewModel();
            this.OneWayBind(ViewModel, x => x.TheGuid, x => x.TheGuid.Text);
        }

        TestViewModel _ViewModel;
        public TestViewModel ViewModel {
            get { return _ViewModel; }
            set { this.RaiseAndSetIfChanged(ref _ViewModel, value); }
        }

        object IViewFor.ViewModel {
            get { return ViewModel; }
            set { ViewModel = (TestViewModel)value; }
        }

        public TextView TheGuid { get; protected set; }
    }
}


