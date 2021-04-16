using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    public interface IDataPropertyManager
    {
        void Register<TProperty, TManager>() where TProperty : DataProperty, IDataProperty where TManager : IDataPropertyHandler<TProperty>;
        void Unregister<TProperty>() where TProperty : DataProperty, IDataProperty;
        TManager GetManager<TProperty, TManager>() where TProperty : DataProperty, IDataProperty where TManager : IDataPropertyHandler<TProperty>;
    }
}
