using Acorisoft.Properties;
using Acorisoft.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Dialogs
{
    class DialogManager : IDialogManager
    {
        private readonly IFullLogger        _Logger;
        private readonly Stack<object>      _UndoStack;
        private readonly Stack<object>      _RedoStack;

        public DialogManager(ILogManager manager)
        {
            _Logger = manager.GetLogger(this.GetType());
            _UndoStack = new Stack<object>();
            _RedoStack = new Stack<object>();
        }

        protected static IViewModel GetViewModel<T>() where T : IViewModel
        {
            return Locator.Current.GetService<T>();
        }

        #region Confirm


        public Task<bool> Confirm(string title, string content)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Confirm(string title, object content)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Confirm<TViewModel>(string title) where TViewModel : IViewModel
        {
            throw new NotImplementedException();
        }

        #endregion Confirm

        #region Notification

        public Task Notification(string title, string content)
        {
            throw new NotImplementedException();
        }

        public Task Notification(string title, object content)
        {
            throw new NotImplementedException();
        }

        public Task Notification<TViewModel>(string title) where TViewModel : IViewModel
        {
            throw new NotImplementedException();
        }

        #endregion Notification

        #region Dialog

        public Task<IDialogSession> Dialog<TViewModel>() where TViewModel : IViewModel
        {
            throw new NotImplementedException();
        }

        public Task<IDialogSession> Dialog(IViewModel vm)
        {
            throw new NotImplementedException();
        }

        #endregion Dialog

        #region Step ViewModels

        public Task<IDialogSession> Step<TStep1, TStep2>()
            where TStep1 : IViewModel
            where TStep2 : IViewModel
        {
            var ViewModels = new []
            {
                GetViewModel<TStep1>(),
                GetViewModel<TStep2>(),
            };

            if (ViewModels.All(x => x is not null))
            {
                return Step(ViewModels);
            }
            else
            {
                _Logger.Error(SR.DialogManager_Step_ManyViewModel_NotRegister);
                throw new InvalidOperationException(SR.DialogManager_Step_ManyViewModel_NotRegister);
            }
        }

        public Task<IDialogSession> Step<TStep1, TStep2, TStep3>()
            where TStep1 : IViewModel
            where TStep2 : IViewModel
            where TStep3 : IViewModel
        {
            var ViewModels = new []
            {
                GetViewModel<TStep1>(),
                GetViewModel<TStep2>(),
                GetViewModel<TStep3>(),
            };

            if (ViewModels.All(x => x is not null))
            {
                return Step(ViewModels);
            }
            else
            {
                _Logger.Error(SR.DialogManager_Step_ManyViewModel_NotRegister);
                throw new InvalidOperationException(SR.DialogManager_Step_ManyViewModel_NotRegister);
            }
        }

        public Task<IDialogSession> Step<TStep1, TStep2, TStep3, TStep4>()
            where TStep1 : IViewModel
            where TStep2 : IViewModel
            where TStep3 : IViewModel
            where TStep4 : IViewModel
        {
            var ViewModels = new []
            {
                GetViewModel<TStep1>(),
                GetViewModel<TStep2>(),
                GetViewModel<TStep3>(),
                GetViewModel<TStep4>(),
            };

            if (ViewModels.All(x => x is not null))
            {
                return Step(ViewModels);
            }
            else
            {
                _Logger.Error(SR.DialogManager_Step_ManyViewModel_NotRegister);
                throw new InvalidOperationException(SR.DialogManager_Step_ManyViewModel_NotRegister);
            }
        }

        public Task<IDialogSession> Step<TStep1, TStep2, TStep3, TStep4, TStep5>()
            where TStep1 : IViewModel
            where TStep2 : IViewModel
            where TStep3 : IViewModel
            where TStep4 : IViewModel
            where TStep5 : IViewModel
        {
            var ViewModels = new []
            {
                GetViewModel<TStep1>(),
                GetViewModel<TStep2>(),
                GetViewModel<TStep3>(),
                GetViewModel<TStep4>(),
                GetViewModel<TStep5>(),
            };

            if (ViewModels.All(x => x is not null))
            {
                return Step(ViewModels);
            }
            else
            {
                _Logger.Error(SR.DialogManager_Step_ManyViewModel_NotRegister);
                throw new InvalidOperationException(SR.DialogManager_Step_ManyViewModel_NotRegister);
            }
        }

        public Task<IDialogSession> Step<TStep1, TStep2, TStep3, TStep4, TStep5, TStep6>()
            where TStep1 : IViewModel
            where TStep2 : IViewModel
            where TStep3 : IViewModel
            where TStep4 : IViewModel
            where TStep5 : IViewModel
            where TStep6 : IViewModel
        {
            var ViewModels = new []
            {
                GetViewModel<TStep1>(),
                GetViewModel<TStep2>(),
                GetViewModel<TStep3>(),
                GetViewModel<TStep4>(),
                GetViewModel<TStep5>(),
                GetViewModel<TStep6>(),
            };

            if (ViewModels.All(x => x is not null))
            {
                return Step(ViewModels);
            }
            else
            {
                _Logger.Error(SR.DialogManager_Step_ManyViewModel_NotRegister);
                throw new InvalidOperationException(SR.DialogManager_Step_ManyViewModel_NotRegister);
            }
        }

        public Task<IDialogSession> Step<TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TStep7>()
            where TStep1 : IViewModel
            where TStep2 : IViewModel
            where TStep3 : IViewModel
            where TStep4 : IViewModel
            where TStep5 : IViewModel
            where TStep6 : IViewModel
            where TStep7 : IViewModel
        {
            var ViewModels = new []
            {
                GetViewModel<TStep1>(),
                GetViewModel<TStep2>(),
                GetViewModel<TStep3>(),
                GetViewModel<TStep4>(),
                GetViewModel<TStep5>(),
                GetViewModel<TStep6>(),
                GetViewModel<TStep7>(),
            };

            if (ViewModels.All(x => x is not null))
            {
                return Step(ViewModels);
            }
            else
            {
                _Logger.Error(SR.DialogManager_Step_ManyViewModel_NotRegister);
                throw new InvalidOperationException(SR.DialogManager_Step_ManyViewModel_NotRegister);
            }
        }

        public Task<IDialogSession> Step<TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TStep7, TStep8>()
            where TStep1 : IViewModel
            where TStep2 : IViewModel
            where TStep3 : IViewModel
            where TStep4 : IViewModel
            where TStep5 : IViewModel
            where TStep6 : IViewModel
            where TStep7 : IViewModel
            where TStep8 : IViewModel
        {
            var ViewModels = new []
            {
                GetViewModel<TStep1>(),
                GetViewModel<TStep2>(),
                GetViewModel<TStep3>(),
                GetViewModel<TStep4>(),
                GetViewModel<TStep5>(),
                GetViewModel<TStep6>(),
                GetViewModel<TStep7>(),
                GetViewModel<TStep8>(),
            };

            if(ViewModels.All(x => x is not null))
            {
                return Step(ViewModels);
            }
            else
            {
                _Logger.Error(SR.DialogManager_Step_ManyViewModel_NotRegister);
                throw new InvalidOperationException(SR.DialogManager_Step_ManyViewModel_NotRegister);
            }
        }

        public Task<IDialogSession> Step(IEnumerable<IViewModel> stepViewModels)
        {
            throw new NotImplementedException();
        }


        #endregion Step ViewModels

        public event Action<IViewModel> DialogChanged;
    }
}
