using System;
using Android.App;
using Android.Runtime;
using ReactiveUI;
using Starter.Core.ViewModels;

namespace Starter
{
    [Application(Label = "Starter-Android")]
    public class AndroidApplication : Application
    {
        AutoSuspendHelper autoSuspendHelper;
        public AndroidApplication(IntPtr handle, JniHandleOwnership transfer) : base(handle,transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
                        
            autoSuspendHelper = new AutoSuspendHelper(this);
            RxApp.SuspensionHost.CreateNewAppState = () => {
                return new AppBootstrapper();
            };

            RxApp.SuspensionHost.SetupDefaultSuspendResume();
        }
    }
}

