using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using Starter.Core.ViewModels;
using Akavache;

namespace Starter.Views
{
    [Activity (Label = "Starter-Android", MainLauncher = true)]
    public class TestActivity : ReactiveActivity<TestViewModel>
    {
        int count = 1;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            myButton.Events().Click.Subscribe(_ => myButton.Text = string.Format("{0} clicks!", count++));

            this.OneWayBind(ViewModel, x => x.TheGuid, x => x.TheGuid.Text);

            ViewModel = await BlobCache.LocalMachine.GetOrCreateObject("TestViewModel", () => {
                return new TestViewModel();
            });
        }

        public TextView TheGuid {
            get { return this.GetControl<TextView>(); }
        }

        public Button myButton { 
            get { return this.GetControl<Button>(); }
        }
    }
}