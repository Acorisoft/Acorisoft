using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    public abstract class Selectable : Bindable
    {
        private bool _isSelected;
        private bool _canSelected;


        public bool IsSelected
        {
            get => _isSelected;
            set => Set(ref _isSelected, value);
        }

        public bool CanSelected
        {
            get => _canSelected;
            set => Set(ref _canSelected, value);
        }
    }
}
