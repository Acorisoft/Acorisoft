using System.Collections.ObjectModel;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;

namespace Acorisoft.Studio.ViewModels
{
    public class PageItemCountViewModel : DialogViewModelBase
    {
        private static readonly ObservableCollection<int> ItemCountPerPage = new ObservableCollection<int>
        {
            10, 15, 20, 25, 30, 35, 40, 45, 50, 60, 70, 80, 90, 100
        };
        
        public override bool VerifyAccess()
        {
            return _selectedItemCount > 0 && _selectedItemCount < 100;
        }

        private int _selectedItemCount;

        /// <summary>
        /// 当前选择的
        /// </summary>
        public int SelectedItemCount
        {
            get => _selectedItemCount;
            set => Set(ref _selectedItemCount, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<int> Samples => ItemCountPerPage;
    }
}