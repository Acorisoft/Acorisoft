using System;
using Acorisoft.Extensions.Windows.ViewModels;

namespace Acorisoft.Extensions.Windows
{
    public class InteractiveService : IInteractiveService
    {
        private readonly IQuickViewModel[] _ixs = new IQuickViewModel[4];
        
        public void SetQuickView(IQuickViewModel quickViewModel)
        {
            _ixs[0] = quickViewModel ?? throw new ArgumentNullException(nameof(quickViewModel));
            FireEvent();
        }

        public void SetContextualView(IQuickViewModel quickViewModel)
        {
            _ixs[1] = quickViewModel ?? throw new ArgumentNullException(nameof(quickViewModel));
            FireEvent();
        }

        public void SetToolView(IQuickViewModel quickViewModel)
        {
            _ixs[2] = quickViewModel ?? throw new ArgumentNullException(nameof(quickViewModel));
            FireEvent();
        }

        public void SetExtraView(IQuickViewModel quickViewModel)
        {
            _ixs[3] = quickViewModel ?? throw new ArgumentNullException(nameof(quickViewModel));
            FireEvent();
        }

        private void FireEvent()
        {
            Changed?.Invoke(this, new IxContentChangedEventArgs(_ixs[0], _ixs[2], _ixs[1], _ixs[3]));
        }

        public event IxContentChangedEventHandler Changed;
    }
}