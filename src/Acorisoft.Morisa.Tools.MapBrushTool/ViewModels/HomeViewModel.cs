using Acorisoft.Morisa.Dialogs;
using Acorisoft.Morisa.Map;
using Acorisoft.Morisa.ViewModels;
using DynamicData;
using DynamicData.Binding;
using GongSolutions.Wpf.DragDrop;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Acorisoft.Morisa.Tools.ViewModels
{
    public partial class HomeViewModel : ViewModelBase
    {
        private IMapGroupAdapter _SelectedGroup;
        private protected IMapBrushSetFactory _Factory;
        private protected ReadOnlyObservableCollection<IMapBrush> _BrushCollection;
        private protected ReadOnlyObservableCollection<IMapGroupAdapter> _GroupCollection;
        private readonly ICommand                       _AddBrushOperator;
        private readonly ICommand                       _RemoveThisGroupBrushOperator;
        private readonly ICommand                       _ClearAllBrushOperator;
        private readonly ICommand                       _AddGroupOperator;
        private readonly ICommand                       _RemoveThisGroupOperator;
        private readonly ICommand                       _ClearAllGroupOperator;
        private readonly ICommand                       _SelectedGroupOperator;

        public HomeViewModel(IMapBrushSetFactory factory)
        {
            _Factory = factory;
            _BrushCollection = factory.BrushCollection;
            _GroupCollection = factory.GroupCollection;
            _SelectedGroupOperator = ReactiveCommand.Create<object>(x =>
            {
                if (x is MapGroupAdapter adapter)
                {
                    SelectedGroup = adapter;
                }
            });

            _AddGroupOperator = ReactiveCommand.Create<MapGroupAdapter>(OnAddGroupOperator, _Factory.IsOpen);
            _RemoveThisGroupOperator = ReactiveCommand.Create<MapGroupAdapter>(OnRemoveThisGroupOperator, _Factory.IsOpen);
        }

        protected async void OnAddGroupOperator(MapGroupAdapter parent)
        {
            Debug.WriteLine($"parent is null :{parent == null}");
            var session = await DialogManager.Dialog(new NewBrushGroupViewFunction(parent?.Source));

            if (session.IsCompleted && session.GetResult<IMapGroup>() is IMapGroup newGroup)
            {
                if (parent != null)
                {
                    parent.Children.Add(new MapGroupAdapter(newGroup));
                }
                else
                {
                    _Factory.MapGroupSource.Add(new MapGroupAdapter(newGroup));
                }
            }
        }

        protected async void OnRemoveGroupOperator()
        {

        }

        protected async void OnRemoveThisGroupOperator(IMapGroup group)
        {

        }

        protected async void OnRemvoeBrushOperator()
        {

        }

        public IMapGroupAdapter SelectedGroup
        {
            get => _SelectedGroup;
            set => Set(ref _SelectedGroup, value);
        }
        public ICommand SelectedGroupOperator => _SelectedGroupOperator;
        public ReadOnlyObservableCollection<IMapBrush> BrushCollection => _BrushCollection;
        public ReadOnlyObservableCollection<IMapGroupAdapter> GroupCollection => _GroupCollection;
        /// <summary>
        /// 
        /// </summary>
        public ICommand AddBrushOperator => _AddBrushOperator;

        /// <summary>
        /// 
        /// </summary>
        public ICommand AddGroupOperator => _AddGroupOperator;

        /// <summary>
        /// 
        /// </summary>
        public ICommand RemoveThisGroupBrushOperator => _RemoveThisGroupBrushOperator;

        /// <summary>
        /// 
        /// </summary>
        public ICommand RemoveThisGroupOperator => _RemoveThisGroupOperator;

        /// <summary>
        /// 
        /// </summary>
        public ICommand ClearBrushOperator => _ClearAllBrushOperator;

        /// <summary>
        /// 
        /// </summary>
        public ICommand ClearGroupOperator => _ClearAllGroupOperator;

    }
}