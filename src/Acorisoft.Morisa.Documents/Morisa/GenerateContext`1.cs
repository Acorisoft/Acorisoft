using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public class GenerateContext<T> : IGenerateContext<T> where T : class, IProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public T Context { get; set; }
    }
}
