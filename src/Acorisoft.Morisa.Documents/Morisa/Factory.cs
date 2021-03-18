using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acorisoft.Morisa.Emotions;
using Acorisoft.Properties;

namespace Acorisoft.Morisa
{
    public static class Factory
    {
        //-------------------------------------------------------------------------------------------------
        //
        //  GenereateGuid
        //
        //-------------------------------------------------------------------------------------------------
        public static string GenereateGuid() => Guid.NewGuid().ToString("N");

        [Obsolete]
        //-------------------------------------------------------------------------------------------------
        //
        //  CreateMottoEmotion
        //
        //-------------------------------------------------------------------------------------------------
        public static MottoEmotion CreateMottoEmotion()
        {
            return CreateMottoEmotion(SR.MottoEmotionSampleMotto, SR.MottoEmotionSampleSig, MottoEmotionPresentation.Default);
        }
        [Obsolete]
        public static MottoEmotion CreateMottoEmotion(string motto, string sig)
        {
            return new MottoEmotion
            {
                Id = GenereateGuid(),
                Motto = motto,
                Signature = sig,
                Presentation = MottoEmotionPresentation.Default
            };
        }
        [Obsolete]
        public static MottoEmotion CreateMottoEmotion(string motto, string sig, MottoEmotionPresentation presentation)
        {
            return new MottoEmotion
            {
                Id = GenereateGuid(),
                Motto = motto,
                Signature = sig,
                Presentation = presentation
            };
        }
    }
}
