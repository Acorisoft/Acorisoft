using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.ViewModels
{
    public class InspirationGalleryInsertTextViewModel : DialogViewModel
    {
        private string _text;

        public string Text {
            get => _text;
            set => Set(ref _text, value);
        }
    }
}
