using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Acorisoft.Morisa.Emotions
{
    public class MottoEmotion : EmotionElement, IMottoEmotion
    {
        private string _Motto;
        private string _Signature;
        private MottoEmotionPresentation _Presentation;

        [Column("motto")]
        public string Motto
        {
            get => _Motto;
            set => Set(ref _Motto , value);
        }

        [Column("sig")]
        public string Signature
        {
            get => _Signature;
            set => Set(ref _Signature , value);
        }

        [Column("p")]
        public MottoEmotionPresentation Presentation
        {
            get => _Presentation;
            set => Set(ref _Presentation , value);
        }
    }
}
