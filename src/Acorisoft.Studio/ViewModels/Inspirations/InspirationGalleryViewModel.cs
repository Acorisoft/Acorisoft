using System;
using System.Threading.Tasks;
using System.Windows;
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
            OpenThisCommand = ReactiveCommand.Create<InspirationIndexWrapper>(OnOpen, System.IsOpen);
            NewInspirationFromClipboardCommand = ReactiveCommand.Create(OnNewInspirationFromClipboard, System.IsOpen);
        }

        protected async void OnNew()
        {
            var info = new NewItemInfo<InspirationDocument, InspirationIndex>
            {
                Item = new ConversationInspiration
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
                info.Item
            );

            //
            // 跳转
            ViewAware.NavigateTo<ConversationInspirationViewModel>(param);
        }

        public Task NewAsync(INewInspirationInfo info)
        {
            return Task.Run(async () =>
            {
                await Engine.NewAsync(info);

                //
                //
                var param =
                    new GalleryViewModelParameter<InspirationIndex, InspirationIndexWrapper, InspirationDocument>(
                        new InspirationIndexWrapper(info.FeedBackValue1),
                        info.FeedBackValue1,
                        info.Item
                    );

                //
                // 跳转
                ViewAware.NavigateTo<ConversationInspirationViewModel>(param);
            });
        }

        protected async void OnNewInspirationFromClipboard()
        {
            try
            {
                if (Clipboard.ContainsImage())
                {
                    var image = Clipboard.GetImage();
                    var pasteFromImage = new InspirationClipboardImageViewModel(image);
                    var session = await ViewAware.ShowDialog(pasteFromImage);
                    if (session.IsCompleted && session.GetResult<INewInspirationInfo>() is INewInspirationInfo info)
                    {
                        await NewAsync(info);
                    }
                }
                else if (Clipboard.ContainsText())
                {
                    var pasteFromText = new InspirationClipboardTextViewModel(Clipboard.GetText());
                    var session = await ViewAware.ShowDialog(pasteFromText);
                    if (session.IsCompleted && session.GetResult<INewInspirationInfo>() is INewInspirationInfo info)
                    {
                        await NewAsync(info);
                    }
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        protected async void OnOpen(InspirationIndexWrapper wrapper)
        {
            var document = await Engine.OpenAsync(wrapper.Source);
            var param = new GalleryViewModelParameter<InspirationIndex, InspirationIndexWrapper, InspirationDocument>(
                wrapper,
                wrapper.Source,
                document
            );

            //
            // 跳转
            ViewAware.NavigateTo<ConversationInspirationViewModel>(param);
        }

        public ICommand NewCommand { get; }
        public ICommand OpenThisCommand { get; }
        public ICommand NewInspirationFromClipboardCommand { get; }
    }
}