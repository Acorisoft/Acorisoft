using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Moniker
{
    /// <summary>
    /// <see cref="IMoniker"/>
    /// </summary>
    public interface IMoniker
    {
        /// <summary>
        /// 
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Color { get; set; }
    }
}
