using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    static class Helper
    {
        public static LiteDatabase GetDatabase(ILoadContext context)
        {
            return new LiteDatabase(new ConnectionString
            {
                Filename = context.FileName,
            });
        }
    }
}
