using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Emotions
{
    public class MottoEmotion : EmotionElement, IMottoEmotion
    {
        private string _Motto;
        private string _Signature;
        private MottoEmotionPresentation _Presentation;

        public string Motto
        {
            get => _Motto;
            set => Set(ref _Motto , value);
        }

        public string Signature
        {
            get => _Signature;
            set => Set(ref _Signature , value);
        }

        public MottoEmotionPresentation Presentation
        {
            get => _Presentation;
            set => Set(ref _Presentation , value);
        }
    }
}
