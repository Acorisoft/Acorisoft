using LiteDB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.v2
{
    [Obsolete]
    [DebuggerDisplay("{FileName}")]
    public class CompositionSetStore : /*ICompositionSetStore,*/IEquatable<CompositionSetStore>
    {
        public bool Equals(CompositionSetStore y)
        {
            return FileName == y.FileName;
        }

        public override sealed bool Equals(object obj)
        {
            if(obj is CompositionSetStore y)
            {
                return Equals(y);
            }

            return base.Equals(obj);
        }

        public override sealed int GetHashCode()
        {
            if (!string.IsNullOrEmpty(FileName))
            {
                return HashCode.Combine(FileName.GetHashCode(), base.GetHashCode());
            }
            return base.GetHashCode();
        }

        public override sealed string ToString()
        {
            return $"{{{Name},{FileName}}}";
        }

        [BsonId]
        public string Name { get; set; }
        public string Directory { get; set; }
        public string FileName { get; set; }

    }
}
