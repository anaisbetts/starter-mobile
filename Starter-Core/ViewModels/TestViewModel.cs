using System;
using ReactiveUI;
using System.Runtime.Serialization;
using Splat;

namespace Starter.Core.ViewModels
{
    [DataContract]
    public class TestViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment {
            get { return "State Serialization Test"; }
        }

        public IScreen HostScreen { get; protected set; }

        string _TheGuid;
        [DataMember] public string TheGuid {
            get { return _TheGuid; }
            set { this.RaiseAndSetIfChanged(ref _TheGuid, value); }
        }

        public TestViewModel(IScreen hostScreen = null)
        {
            hostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            TheGuid = Guid.NewGuid().ToString();
        }
    }
}