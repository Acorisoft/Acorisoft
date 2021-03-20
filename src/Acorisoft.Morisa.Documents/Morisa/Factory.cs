using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acorisoft.Morisa.Emotions;
using Acorisoft.Morisa.Map;
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
        public static string GenerateId() => Guid.NewGuid().ToString("N");
        public static Guid GenerateGuid() => Guid.NewGuid();

        public static LiteDatabase CreateDatabase(string fileName, long initSize = 4 * 1024 * 1024)
        {
            var database = new LiteDatabase(new ConnectionString
            {
                Filename = fileName,
                InitialSize = initSize,

            });
            return database;
        }

        public static MapGroup CreateMapGroup(string name)
        {
            return new MapGroup
            {
                Id = GenerateGuid(),
                Name = name
            };
        }

        public static MapGroup CreateMapGroup(string name , IMapGroup parent)
        {
            return new MapGroup
            {
                Id = GenerateGuid(),
                OwnerId = parent?.Id,
                Name = name
            };
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
                Id = GenerateId(),
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
                Id = GenerateId(),
                Motto = motto,
                Signature = sig,
                Presentation = presentation
            };
        }
    }
}
