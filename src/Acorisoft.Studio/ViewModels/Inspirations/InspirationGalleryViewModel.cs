using System;
using System.Windows.Input;
using Acorisoft.Extensions.Platforms.Windows;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.Documents.Inspirations;
using Acorisoft.Studio.Documents.StickyNotes;
using Acorisoft.Studio.Engines;
using Acorisoft.Studio.ProjectSystems;
using ReactiveUI;

namespace Acorisoft.Studio.ViewModels
{
    public class InspirationGalleryViewModel : GalleryViewModelBase<InspirationEngine, InspirationIndex,
        InspirationIndexWrapper, InspirationDocument>
    {
        public InspirationGalleryViewModel(IComposeSetSystem system, InspirationEngine engine) : base(system, engine)
        {
            NewCommand = ReactiveCommand.Create(OnNew, System.IsOpen);
        }

        protected async void OnNew()
        {
            var info = new NewItemInfo<InspirationDocument, InspirationIndex, InspirationDocument>
            {
                FeedBackValue2 = new ConversationInspiration
                {
                    CreationTimestamp = DateTime.Now,
                    LastAccessTimestamp = DateTime.Now
                }
            };

            await Engine.NewAsync(info);

            //
            //
            var param = new GalleryViewModelParameter<InspirationIndex, InspirationIndexWrapper, InspirationDocument>(
                new InspirationIndexWrapper(info.FeedBackValue1),
                info.FeedBackValue1,
                info.FeedBackValue2
            );
            
            //
            // 跳转
            ViewAware.NavigateTo<ConversationInspirationViewModel>(param);
        }

        public ICommand NewCommand { get; }
    }
}