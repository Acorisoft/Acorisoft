using Acorisoft.Morisa.Core;
using LiteDB;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    [DebuggerDisplay("{Name}")]
    public class MorisaProjectInfo : ReactiveObject, IMorisaProjectInfo, IMorisaProjectTargetInfo, IEquatable<MorisaProjectInfo>
    {
        public bool Equals(MorisaProjectInfo y)
        {
            return y.Name == Name &&
                   y.FileName == FileName;
        }
        public override sealed bool Equals(object obj)
        {
            if (obj is MorisaProjectInfo y)
            {
                return Equals(y);
            }

            return base.Equals(obj);
        }

        public override sealed int GetHashCode()
        {
            return HashCode.Combine(Name?.GetHashCode() ?? base.GetHashCode() , FileName?.GetHashCode() ?? base.GetHashCode());
        }

        public override sealed string ToString()
        {
            return $"Name = {Name}, FileName = {FileName}";
        }

        /// <summary>
        /// 
        /// </summary>
        [BsonId]
        [Reactive] 
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Reactive]
        public string FileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Reactive] 
        public string Directory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonIgnore]
        [Reactive] 
        public string Summary { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonIgnore]
        [Reactive] 
        public string Topic { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonIgnore]
        [Reactive] 
        public ImageObject Cover { get; set; }
    }
}
