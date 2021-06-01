using System;
using System.Collections.Generic;
using Acorisoft.Extensions.Platforms.ComponentModel;
using LiteDB;

namespace Acorisoft.Studio
{
    public abstract class DocumentIndexWrapper : ObservableObject
    {
        private bool _isLocked;
        private bool _isSelected;

        public bool IsSelected
        {
            get => _isSelected;
            set => SetValue(ref _isSelected, value);
        }

        public bool IsLocked
        {
            get => _isLocked;
            set => SetValue(ref _isLocked, value);
        }
    }
}