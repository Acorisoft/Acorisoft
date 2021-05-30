using System;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Acorisoft.Studio.Documents;
using Acorisoft.Studio.ProjectSystems;
namespace Acorisoft.Studio.Engines
{
    public class ComposeSetSystemTreeModule<TIndex, TIndexWrapper, TComposition> : Extensions.Platforms.Disposable,
        IComposeSetSystemModule
        where TIndex : DocumentIndex
        where TIndexWrapper : DocumentIndexWrapper
        where TComposition : Document
    {
        protected readonly BehaviorSubject<bool> IsOpenStream;
        protected readonly CompositeDisposable Disposable;
        protected bool IsOpenField;

        protected ComposeSetSystemTreeModule(IComposeSetRequestQueue requestQueue)
        {
            Disposable = new CompositeDisposable();
            RequestQueue = requestQueue;
            IsOpenStream = new BehaviorSubject<bool>(false);
        }

        #region IComposeSetSystemModule Interface Implments

        private void NewAsyncImpl(INewItemInfo<TComposition> info)
        {
            //
            // 检测是否已经加载创作集。
            if (!IsOpenField)
            {
                return;
            }

            if (info == null)
            {
                throw new InvalidOperationException(nameof(info));
            }

            if (string.IsNullOrEmpty(info.Name))
            {
                info.Name = SR.ComposeSetSystemModule_EmptyName;
            }

            NewCore(info);
        }

        protected virtual void NewCore(INewItemInfo<TComposition> info)
        {
        }

        /// <summary>
        /// 在一个异步操作中创建一个新的项目。
        /// </summary>
        /// <param name="info">指定要创建的操作。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task NewAsync(INewItemInfo<TComposition> info)
        {
            return Task.Run(() => NewAsyncImpl(info));
        }

        #endregion

        /// <summary>
        /// 当创作集打开时处理数据加载任务。
        /// </summary>
        /// <param name="instruction"></param>
        protected virtual void OnComposeSetOpening(ComposeSetOpenInstruction instruction)
        {
        }

        protected virtual void OnComposeSetClosing(ComposeSetCloseInstruction instruction)
        {
        }

        protected virtual void OnComposeSetSaving(ComposeSetSaveInstruction instruction)
        {
        }

        protected IComposeSetRequestQueue RequestQueue { get; }

        #region INotificationHandler<> Interface Implements

        private void HandleComposeSetOpen(ComposeSetOpenInstruction instruction)
        {
            RequestQueue.Set();
            OnComposeSetOpening(instruction);
            RequestQueue.Unset();
            IsOpenField = true;
            IsOpenStream.OnNext(IsOpenField);
        }

        private void HandleComposeSetClose(ComposeSetCloseInstruction instruction)
        {
            RequestQueue.Set();
            OnComposeSetClosing(instruction);
            RequestQueue.Unset();
            IsOpenField = false;
            IsOpenStream.OnNext(IsOpenField);
        }

        private void HandleComposeSetSave(ComposeSetSaveInstruction instruction)
        {
            RequestQueue.Set();
            OnComposeSetSaving(instruction);
            RequestQueue.Unset();
        }

        /// <summary>
        /// 处理消息推送
        /// </summary>
        /// <param name="instruction">接收的推送。</param>
        /// <param name="cancellationToken">取消当前任务的Token</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task Handle(ComposeSetOpenInstruction instruction, CancellationToken cancellationToken)
        {
            return Task.Run(() => HandleComposeSetOpen(instruction), cancellationToken);
        }

        /// <summary>
        /// 处理消息推送
        /// </summary>
        /// <param name="instruction">接收的推送。</param>
        /// <param name="cancellationToken">取消当前任务的Token</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task Handle(ComposeSetCloseInstruction instruction, CancellationToken cancellationToken)
        {
            return Task.Run(() => HandleComposeSetClose(instruction), cancellationToken);
        }

        /// <summary>
        /// 处理消息推送
        /// </summary>
        /// <param name="instruction">接收的推送。</param>
        /// <param name="cancellationToken">取消当前任务的Token</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task Handle(ComposeSetSaveInstruction instruction, CancellationToken cancellationToken)
        {
            return Task.Run(() => HandleComposeSetSave(instruction), cancellationToken);
        }

        public IObservable<bool> IsOpen => IsOpenStream;

        #endregion
    }
}