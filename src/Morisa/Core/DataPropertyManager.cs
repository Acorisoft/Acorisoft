using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{

#pragma warning disable IDE0034

    public class DataPropertyManager : IDataPropertyManager
    {
        private readonly IContainer Service;
        private readonly Dictionary<Type,Type> ManagerLocator;

        public DataPropertyManager(IContainer container)
        {
            Service = container;
            ManagerLocator = new Dictionary<Type, Type>();
        }

        public TManager GetManager<TProperty, TManager>()
            where TProperty : DataProperty, IDataProperty
            where TManager : IDataPropertyHandler<TProperty>
        {
            if (ManagerLocator.TryGetValue(typeof(TProperty), out var serviceKey))
            {
                return (TManager)Service.Resolve(serviceKey);
            }

            return default(TManager);
        }

        public void Register<TProperty, TManager>()
            where TProperty : DataProperty, IDataProperty
            where TManager : IDataPropertyHandler<TProperty>
        {
            if (ManagerLocator.TryAdd(typeof(TProperty), typeof(TManager)))
            {
                Service.Register(typeof(TManager));
            }
        }

        public void Unregister<TProperty>() where TProperty : DataProperty, IDataProperty
        {
            if (ManagerLocator.ContainsKey(typeof(TProperty)))
            {
                Service.Unregister(typeof(TProperty));
            }
        }
    }
}
