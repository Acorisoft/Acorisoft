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
        private readonly IBrushSetFactory _Factory;
        private readonly ICommand _AddGroupOperator;
        private readonly ICommand _AddRootGroupOperator;
        private readonly ICommand _GroupSelectorOperator; 
        private readonly ICommand _RemoveGroupOperator;


        private IBrushGroupAdapter _SelectedGroup;


        public HomeViewModel()
        {
            _Factory = GetService<IBrushSetFactory>();
            _AddGroupOperator = ReactiveCommand.Create(AddGroupCore);
            _AddRootGroupOperator = ReactiveCommand.Create(AddRootGroupCore);
            _RemoveGroupOperator = ReactiveCommand.Create(RemoveGroupCore);
            _GroupSelectorOperator = ReactiveCommand.Create<IBrushGroupAdapter>(OnSelectedBrushGroup);
        }

        protected async void RemoveGroupCore()
        {
            var session = await Dialog(new Notification{ });

            if (session.IsCompleted)
            {
                _Factory.Remove(_SelectedGroup.Source);
            }
        }

        protected async void AddGroupCore()
        {
            var session = await Dialog<NewBrushGroupDialogViewFunction>();

            if(session.IsCompleted && session.GetResult<IBrushGroup>() is IBrushGroup newGroup)
            {
                newGroup.Id = Factory.GenerateGuid();

                if (_SelectedGroup is null)
                {
                    _Factory.Add(newGroup);
                }
                else
                {
                    _Factory.Add(newGroup, _SelectedGroup.Source);
                }
            }
        }
        protected async void AddRootGroupCore()
        {
            var session = await Dialog<NewBrushGroupDialogViewFunction>();

            if (session.IsCompleted && session.GetResult<IBrushGroup>() is IBrushGroup newGroup)
            {
                newGroup.Id = Factory.GenerateGuid();

                _Factory.Add(newGroup);
            }
        }

        protected void OnSelectedBrushGroup(IBrushGroupAdapter group)
        {
            _SelectedGroup = group;
        }

        public ICommand RemoveGroupOperator => _RemoveGroupOperator;
        public ICommand AddGroupOperator => _AddGroupOperator;
        public ICommand AddRootGroupOperator => _AddRootGroupOperator;
        public ICommand GroupSelectorOperator => _GroupSelectorOperator;

        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyObservableCollection<BrushAdapter> Brushes => _Factory.Brushes;

        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyObservableCollection<BrushGroupAdapter> Groups => _Factory.Groups;
    }
}