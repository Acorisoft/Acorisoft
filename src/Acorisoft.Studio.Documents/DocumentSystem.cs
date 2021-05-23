using Acorisoft.Studio.Documents.Engines;
using Acorisoft.Studio.Documents.ProjectSystem;
using DryIoc;
using MediatR;

namespace Acorisoft.Studio.Documents
{
    public static class DocumentSystem
    {
        public static void UseMorisa(this IContainer container)
        {
            var factory = new ServiceFactory(container.Resolve);
            var mediator = new Mediator(factory);
            container.RegisterInstance<IMediator>(mediator);
            
            //
            //
            var pm = new ProjectManager(container.Resolve<IMediator>());
            container.RegisterInstance(pm);
            container.RegisterInstance<IDocumentEngineAquirement>(pm.Aquirement);
            container.RegisterInstance<IProjectManager>(pm);


            //
            // mockup
            container.Register<INotificationHandler<DocumentCloseNotification>, MockupDocumentEngineA>();
            container.Register<INotificationHandler<DocumentSwitchNotification>, MockupDocumentEngineB>();
            var dea = new MockupDocumentEngineA(container.Resolve<IDocumentEngineAquirement>());
            var deb = new MockupDocumentEngineB(container.Resolve<IDocumentEngineAquirement>());
            container.UseInstance<INotificationHandler<DocumentSwitchNotification>>(dea , IfAlreadyRegistered.AppendNewImplementation);
            container.UseInstance<INotificationHandler<DocumentSwitchNotification>>(deb, IfAlreadyRegistered.AppendNewImplementation);
            container.UseInstance<INotificationHandler<DocumentCloseNotification>>(dea, IfAlreadyRegistered.AppendNewImplementation);
            container.UseInstance<INotificationHandler<DocumentCloseNotification>>(deb, IfAlreadyRegistered.AppendNewImplementation);
        }
    }
}