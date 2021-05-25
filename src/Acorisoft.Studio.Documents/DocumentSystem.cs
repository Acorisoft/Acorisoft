using System.Data;
using Acorisoft.Studio.Documents.ProjectSystem;
using DryIoc;
using MediatR;

namespace Acorisoft.Studio.Documents
{
    public static class DocumentSystem
    {
        public static IContainer UseMorisa(this IContainer container)
        {
            var factory = new ServiceFactory(container.Resolve);
            var mediator = new Mediator(factory);
            var csQueue = new CompositionSetRequestQueue();
            var csFileManager = new CompositionSetFileManager(csQueue);
            container.RegisterInstance<ICompositionSetRequestQueue>(csQueue);
            container.RegisterInstance<IMediator>(mediator);
            container.RegisterInstance<ICompositionSetFileManager>(csFileManager);
            container.RegisterInstance<ICompositionSetPropertyManager>(new CompositionSetPropertyManager(csFileManager));

            var csm = new CompositionSetManager(container.Resolve<IMediator>(), container.Resolve<ICompositionSetPropertyManager>());
            //
            //
            container.RegisterInstance<ICompositionSetManager>(csm);
            return container;
        }
    }
}