using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acorisoft.Morisa.Emotions;
using Acorisoft.Properties;
using LiteDB;

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

        public static LiteDatabase CreateDatabase(string fileName, long initSize = 4 * 1024 * 1024)
        {
            var database = new LiteDatabase(new ConnectionString
            {
                Filename = fileName,
                InitialSize = initSize,

            });
            return database;
        }

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
