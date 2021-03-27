using Acorisoft.Morisa.Dialogs;
using Acorisoft.Morisa.Map;
using Acorisoft.Morisa.Tools.Models;
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
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Acorisoft.Morisa.Tools.ViewModels
{
    public partial class HomeViewModel : ViewModelBase
    {
        private readonly IBrushSetFactory   _Factory;
        private readonly ICommand           _AddGroupOperator;
        private readonly ICommand           _AddRootGroupOperator;
        private readonly ICommand           _RenameGroupOperator;
        private readonly ICommand           _RemoveGroupOperator;
        private readonly ICommand           _RemoveAllGroupOperator;
        private readonly ICommand           _AddBrushToGroupOperator;
        private readonly ICommand           _AddBrushesToGroupOperator;
        private readonly ICommand           _GroupSelectorOperator;
        private readonly ICommand           _RemoveBrushesFromGroupOperator;
        private readonly ICommand           _RemoveBrushFromGroupOperator;
        private readonly ICommand           _SelectAllBrushesOperator;
        private readonly ICommand           _LockGroupOperator;
        private readonly ICommand           _UnlockGroupOperator;
        private readonly ICommand           _SignAsElementOperator;
        private readonly ICommand           _UnsignAsElementOperator;
        private IBrushGroupAdapter          _SelectedGroup;
        private bool                        _IsSelected;

        private readonly BehaviorSubject<bool>       _SelectedGroupObservable;


        public HomeViewModel()
        {
            _Factory = GetService<IBrushSetFactory>();

            _IsSelected = true;
            _SelectedGroupObservable = new BehaviorSubject<bool>(false);

            var openAndSelectedGroup = Observable.CombineLatest(_SelectedGroupObservable, _Factory.IsOpen,( x , y)=> x && y);


            //
            // 必须打开数据集并且选择一个分组

            _LockGroupOperator = ReactiveCommand.Create(LockGroupCore, openAndSelectedGroup);
            _UnlockGroupOperator = ReactiveCommand.Create(UnlockGroupCore, openAndSelectedGroup);
            //
            // 必须打开数据集并且选择一个分组
            //
            // 必须打开数据集并且选择一个分组
            _AddGroupOperator = ReactiveCommand.Create(AddGroupCore, openAndSelectedGroup);

            //
            // 必须选择打开数据集
            _AddRootGroupOperator = ReactiveCommand.Create(AddRootGroupCore, _Factory.IsOpen);

            //
            // 必须打开数据集并且选择一个分组
            _AddBrushToGroupOperator = ReactiveCommand.Create(AddBrushToGroupCore, openAndSelectedGroup);


            //
            // 必须打开数据集并且选择一个分组
            _AddBrushesToGroupOperator = ReactiveCommand.Create(AddBrushesToGroupCore, openAndSelectedGroup);

            _RenameGroupOperator = ReactiveCommand.Create(RenameGroupCore, openAndSelectedGroup);
            //
            // 必须打开数据集并且选择一个分组
            _RemoveGroupOperator = ReactiveCommand.Create(RemoveGroupCore, openAndSelectedGroup);

            //
            //
            _RemoveAllGroupOperator = ReactiveCommand.Create(RemoveAllGroupCore, _Factory.IsOpen);

            //
            //
            _RemoveBrushesFromGroupOperator = ReactiveCommand.Create(RemoveBrushesFromGroupCore, openAndSelectedGroup);

            //
            //
            _RemoveBrushFromGroupOperator = ReactiveCommand.Create(RemoveBrushFromGroupCore, openAndSelectedGroup);

            //
            // 随时
            _GroupSelectorOperator = ReactiveCommand.Create<IBrushGroupAdapter>(OnSelectedBrushGroup);

            //
            //
            _SelectAllBrushesOperator = ReactiveCommand.Create(SelectAllBrushesCore, openAndSelectedGroup);

            _SignAsElementOperator = ReactiveCommand.Create(() =>
            {
                if(_SelectedGroup == null)
                {
                    return;
                }

                _SelectedGroup.IsElement = true;
                _Factory.Update(_SelectedGroup.Source);
            }, openAndSelectedGroup);

            _UnsignAsElementOperator = ReactiveCommand.Create(() =>
            {
                if (_SelectedGroup == null)
                {
                    return;
                }

                _SelectedGroup.IsElement = false;
                _Factory.Update(_SelectedGroup.Source);
            }, openAndSelectedGroup);
        }

        protected void SelectAllBrushesCore()
        {
            foreach (var brush in _Factory.Brushes.Where(x => x.ParentId == _SelectedGroup.Id))
            {
                brush.IsSelected = _IsSelected;
            }

            _IsSelected = !_IsSelected;
        }

        protected async void RenameGroupCore()
        {
            var session = await Dialog<NewBrushGroupDialogViewFunction>();

            if (session.IsCompleted && session.GetResult<IBrushGroup>() is IBrushGroup newGroup)
            {
                _SelectedGroup.Name = newGroup.Name;
                _Factory.Update(_SelectedGroup.Source);
            }
        }

        protected async void AddBrushToGroupCore()
        {
            //
            // 添加到选择的
            var session = await Dialog<NewBrushDialogViewFunction>();
            if (session.IsCompleted && session.GetResult<BrushGenerateContext>() is BrushGenerateContext context)
            {
                var parentGroup = _SelectedGroup;
                var brush = context.Context;
                brush.ParentId = parentGroup.Id;
                _Factory.Add(context, _SelectedGroup.Source, context.LandColor);
            }
        }

        protected async void AddBrushesToGroupCore()
        {
            //
            // 添加到选择的
            var session = await Dialog<NewBrushesDialogViewFunction>();
            if (session.IsCompleted && session.GetResult<BrushesGenerateContext>() is BrushesGenerateContext context)
            {
                var parentGroup = _SelectedGroup;
                foreach (var brushContext in context.Context)
                {
                    var brush = brushContext.Context;
                    brush.ParentId = parentGroup.Id;
                }
                _Factory.Add(context.Context, _SelectedGroup.Source, context.LandColor);
            }
        }

        protected async void RemoveGroupCore()
        {
            var session = await Dialog(new Notification{ });

            if (session.IsCompleted)
            {
                RemoveGroupRecusive(_SelectedGroup);
            }
        }

        protected void RemoveGroupRecusive(IBrushGroupAdapter group)
        {
            if (group.Children.Count > 0)
            {
                foreach (var child in group.Children)
                {
                    RemoveGroupRecusive(child);
                }
            }
            else
            {

                _Factory.Remove(_SelectedGroup.Source);
                _Factory.Remove(_Factory.Brushes.Where(x => x.ParentId == _SelectedGroup.Id).Select(x => x.Source).ToArray());
            }
        }

        protected async void RemoveBrushFromGroupCore()
        {
            var session = await Dialog(new Notification{ });

            if (session.IsCompleted)
            {
                _Factory.Remove(_Factory.Brushes.Where(x => x.IsSelected).Select(x => x.Source).ToArray());
            }
        }

        protected async void RemoveBrushesFromGroupCore()
        {
            var session = await Dialog(new Notification{ });

            if (session.IsCompleted)
            {
                _Factory.Remove(_Factory.Brushes.Where(x => x.ParentId == _SelectedGroup.Id).Select(x => x.Source).ToArray());
            }
        }

        protected void LockGroupCore()
        {
            _SelectedGroup.IsLocked = true;
            _Factory.Update(_SelectedGroup.Source);
        }

        protected void UnlockGroupCore()
        {
            _SelectedGroup.IsLocked = false;
            _Factory.Update(_SelectedGroup.Source);
        }

        protected async void RemoveAllGroupCore()
        {
            var session = await Dialog(new Notification{ });

            if (session.IsCompleted)
            {
                _Factory.RemoveAllBrushes();
                _Factory.RemoveAllGroups();
            }
        }

        protected async void AddGroupCore()
        {
            var session = await Dialog<NewBrushGroupDialogViewFunction>();

            if (session.IsCompleted && session.GetResult<IBrushGroup>() is IBrushGroup newGroup)
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
            if(group == null)
            {
                return;
            }
            _SelectedGroup = group;
            _SelectedGroupObservable.OnNext(group is IBrushGroupAdapter);
            _Factory.FilterStream.OnNext(x => x.ParentId == group.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand UnsignAsElementOperator => _UnsignAsElementOperator;

        /// <summary>
        /// 
        /// </summary>
        public ICommand SignAsElementOpeator => _SignAsElementOperator;

        /// <summary>
        /// 
        /// </summary>
        public ICommand LockGroupOperator => _LockGroupOperator;

        /// <summary>
        /// 
        /// </summary>
        public ICommand UnlockGroupOperator => _UnlockGroupOperator;

        /// <summary>
        /// 
        /// </summary>
        public ICommand RenameGroupOperator => _RenameGroupOperator;

        /// <summary>
        /// 
        /// </summary>
        public ICommand SelectAllBrushesOperator => _SelectAllBrushesOperator;
        /// <summary>
        /// 
        /// </summary>
        public ICommand RemoveBrushFromGroupOperator => _RemoveBrushFromGroupOperator;

        /// <summary>
        /// 
        /// </summary>
        public ICommand RemoveBrushesFromGroupOperator => _RemoveBrushesFromGroupOperator;

        /// <summary>
        /// 
        /// </summary>
        public ICommand AddBrushToGroupOperator => _AddBrushToGroupOperator;

        /// <summary>
        /// 
        /// </summary>
        public ICommand AddBrushesToGroupOperator => _AddBrushesToGroupOperator;

        /// <summary>
        /// 
        /// </summary>
        public ICommand RemoveGroupOperator => _RemoveGroupOperator;
        /// <summary>
        /// 
        /// </summary>
        public ICommand RemoveAllGroupOperator => _RemoveAllGroupOperator;

        /// <summary>
        /// 
        /// </summary>
        public ICommand AddGroupOperator => _AddGroupOperator;

        /// <summary>
        /// 
        /// </summary>
        public ICommand AddRootGroupOperator => _AddRootGroupOperator;

        /// <summary>
        /// 
        /// </summary>
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