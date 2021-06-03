using System;
using System.Collections;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Acorisoft.Extensions.Platforms.Windows;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.Documents.Inspirations;
using Acorisoft.Studio.Resources;
using Acorisoft.Studio.Documents.StickyNotes;
using Acorisoft.Studio.Engines;
using Acorisoft.Studio.ProjectSystems;
using ReactiveUI;

namespace Acorisoft.Studio.ViewModels
{
    public class InspirationGalleryViewModel : GalleryViewModelBase<InspirationEngine, InspirationIndex,
        InspirationIndexWrapper, InspirationDocument>
    {
        //-----------------------------------------------------------------------
        //
        //  Purpose Processors
        //
        //-----------------------------------------------------------------------

        #region Purpose Processors

        public abstract class PurposeProcessor<TData, TViewModel> : INavigatePurposeProcessor
            where TViewModel : PageViewModelBase, IPageViewModel
        {
            public bool IsAccept(object data)
            {
                return data is TData;
            }

            public void Navigate(Hashtable parameter)
            {
                ViewAware.NavigateTo<TViewModel>(parameter);
            }
        }

        public sealed class StickNotePurpose: PurposeProcessor<StickyNoteInspiration, StickyNoteViewModel>
        {
            
        }

        // ReSharper disable once RedundantArrayCreationExpression
        //
        // 目的跳转处理器
        public static readonly INavigatePurposeProcessor[] PurposeProcessors = new INavigatePurposeProcessor[]
        {
            new StickNotePurpose()
        };
        
        #endregion


        public InspirationGalleryViewModel(IComposeSetSystem system, InspirationEngine engine) : base(system, engine)
        {
            NewCommand = ReactiveCommand.Create(OnNew, System.IsOpen);
            OpenThisCommand = ReactiveCommand.Create<InspirationIndexWrapper>(OnOpen, System.IsOpen);
            NewInspirationFromClipboardCommand = ReactiveCommand.Create(OnNewInspirationFromClipboard, System.IsOpen);
        }

        //-----------------------------------------------------------------------
        //
        //  NewAsync / OnNew
        //
        //-----------------------------------------------------------------------
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
                info.FeedBackValue1,
                new InspirationIndexWrapper(info.FeedBackValue1),
                info.Item
            );

            //
            // 跳转
            foreach (var processor in PurposeProcessors)
            {
                if (processor.IsAccept(info.Item))
                {
                    processor.Navigate(param);
                }
            }
        }

        public Task NewAsync(INewItemInfo<InspirationDocument,InspirationIndex> info)
        {
            return Task.Run(async () =>
            {
                await Engine.NewAsync(info);

                //
                //
                var param = ViewModelParameter.Parameter(
                    info.FeedBackValue1,
                    new InspirationIndexWrapper(info.FeedBackValue1),
                    info.Item
                );

                //
                // 跳转
                foreach (var processor in PurposeProcessors)
                {
                    if (processor.IsAccept(info.Item))
                    {
                        processor.Navigate(param);
                    }
                }
            });
        }

        protected async void OnNewInspirationFromClipboard()
        {
            try
            {
                if (Clipboard.ContainsImage())
                {
                    //
                    // 获取图片
                    var image = Clipboard.GetImage();
                    
                    //
                    // 创建视图模型
                    var pasteFromImage = new InspirationClipboardImageViewModel(image);
                    
                    //
                    // 等待用户确认
                    var session = await ViewAware.ShowDialog(pasteFromImage);
                    
                    
                    if (!session.IsCompleted)
                    {
                        return;
                    }
                    
                    var info = session.GetResult<INewStickyInfo>();
                    //
                    // 创建图片资源
                    var resource = new AlbumResource();
                    
                    //
                    // 添加图片资源
                    resource.Add(Guid.NewGuid());
                        
                    info.StickyNote.Album = resource;
                        
                    //
                    // 上传文件
                    await System.UploadAsync(resource, Interop.CopyBitmapToStream(image));
                        
                    //
                    // 创建内容
                    await NewAsync(session.GetResult<INewStickyInfo>());
                }
                else if (Clipboard.ContainsText())
                {  
                    //
                    // 创建视图模型
                    var pasteFromText = new InspirationClipboardTextViewModel(Clipboard.GetText());
                    
                    //
                    // 等待用户确认
                    var session = await ViewAware.ShowDialog(pasteFromText);
                    
                    if (!session.IsCompleted)
                    {
                        return;
                    }
                    
                    //
                    // 创建内容
                    await NewAsync(session.GetResult<INewStickyInfo>());
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        //-----------------------------------------------------------------------
        //
        //  NewAsync / OnNew
        //
        //-----------------------------------------------------------------------
        protected async void OnOpen(InspirationIndexWrapper wrapper)
        {
            //
            // 根据Index获取文档
            var document = await Engine.OpenAsync(wrapper.Source);

            //
            // 封装(Encapsulate)跳转参数。
            var param = ViewModelParameter.Parameter(
                wrapper.Source,
                wrapper,
                document
            );

            //
            // 按照目的跳转
            foreach (var processor in PurposeProcessors)
            {
                if (processor.IsAccept(document))
                {
                    processor.Navigate(param);
                }
            }
        }

        //-----------------------------------------------------------------------
        //
        //  SearchAsync / ResetAsync
        //
        //-----------------------------------------------------------------------
        public async void SearchAsync(string keyword)
        {
            await Engine.FindAsync(keyword);
        }
        
        public async void ResetAsync()
        {
            await Engine.ResetAsync();
        }
        
        public ICommand NewCommand { get; }
        public ICommand OpenThisCommand { get; }
        public ICommand NewInspirationFromClipboardCommand { get; }
    }
}