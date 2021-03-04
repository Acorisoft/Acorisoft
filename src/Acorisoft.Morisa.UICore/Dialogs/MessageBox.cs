using Acorisoft.Morisa.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Dialogs
{
    public class MessageBox : DialogViewModel
    {
        private string _title;
        private string _subtitle;
        private object _content;

        public string Title {
            get => _title;
            set => Set(ref _title, value);
        }
        public string Subtitle {
            get => _subtitle;
            set => Set(ref _subtitle, value);
        }

        public object Content {
            get => _content;
            set => Set(ref _content, value);
        }
    }
}
