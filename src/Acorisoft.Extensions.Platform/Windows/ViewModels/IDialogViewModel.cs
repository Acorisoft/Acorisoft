﻿namespace Acorisoft.Extensions.Windows.ViewModels
{
    public interface IDialogViewModel : IViewModel
    {
        bool VerifyAccess();
        
        string Subtitle { get; }
    }
}