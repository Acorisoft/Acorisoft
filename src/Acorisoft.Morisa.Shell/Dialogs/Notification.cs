using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Dialogs
{
    public class Notification : ReactiveObject,IRoutableViewModel
    {
        private string _title;
        private string _subtitle;
        private object _content;

        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }
        public string Subtitle
        {
            get => _subtitle;
            set => this.RaiseAndSetIfChanged(ref _subtitle, value);
        }

        public object Content
        {
            get => _content;
            set => this.RaiseAndSetIfChanged(ref _content, value);
        }

        public string UrlPathSegment => "notification";

        public IScreen HostScreen => Locator.Current.GetService<IScreen>();
    }
}
