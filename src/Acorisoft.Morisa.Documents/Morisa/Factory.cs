using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public static class Factory
    {
        public static Guid GenerateGuid() => Guid.NewGuid();
        public static string GenerateId() => GenerateGuid().ToString("N");
    }
}
