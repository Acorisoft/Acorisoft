using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Inspirations
{
    public class InspirationTextElement : InspirationElement, IInspirationTextElement
    {
        private string _text;
        private string _color;

        protected virtual void OnTextChanged(string oldVal,string newVal)
        {
            _text = newVal;
            RaiseUpdate(nameof(Text));
        }

        public string Text {
            get => _text;
            set {
                OnTextChanged(_text,value);
            }
        }

        public string Color {
            get => _color;
            set => SetValueAndRaiseUpdate(ref _color, value);
        }
    }
}
