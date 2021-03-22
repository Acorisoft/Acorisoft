//using Acorisoft.Morisa.v1.Map;
//using Acorisoft.Morisa.v1.Persistants;
//using DryIoc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Acorisoft.Morisa.v1
//{
//    [Obsolete]
//    public static class MorisaMixins
//    {
//        public static IContainer UseMorisa(this IContainer container)
//        {
//            container.RegisterInstance<IDisposableCollector>(new DisposableCollector());
//            //container.RegisterInstance<IMapBrushSetFactory>(new MapBrushSetFactory(container.Resolve<IDisposableCollector>()));
//            //container.RegisterInstance<ICompositionSetManager>(new CompositionSetManager());
//            //container.RegisterInstance<IEmotionMechanism>(new EmotionMechanism());
//            return container;
//        }
//    }
//}
