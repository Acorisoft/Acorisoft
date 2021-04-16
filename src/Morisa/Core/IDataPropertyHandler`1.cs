using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    public interface IDataPropertyHandler<TProperty> where TProperty : DataProperty, IDataProperty
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="callback"></param>
        void Handle(TProperty property, Callback callback);
    }
}
