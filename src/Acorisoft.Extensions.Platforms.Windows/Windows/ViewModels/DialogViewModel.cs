﻿using Acorisoft.Extensions.Platforms.Windows.ViewModels;

namespace Acorisoft.Extensions.Platforms.Windows.ViewModels
{
    public abstract class DialogViewModelBase : ViewModelBase, IDialogViewModel
    {
        public virtual bool VerifyAccess()
        {
            return false;
        }

        public virtual bool CanCancel()
        {
            return true;
        }
    }
}