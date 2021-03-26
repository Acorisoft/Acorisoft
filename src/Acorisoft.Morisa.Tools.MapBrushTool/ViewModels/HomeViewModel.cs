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
        private readonly ICommand           _AddBrushToGroupOperator;
        private readonly ICommand           _AddBrushesToGroupOperator;
        private readonly ICommand           _GroupSelectorOperator;
        private readonly ICommand           _RemoveGroupOperator;
        private IBrushGroupAdapter          _SelectedGroup;
        private BehaviorSubject<bool>       _SelectedGroupObservable;


        public HomeViewModel()
        {
            _Factory = GetService<IBrushSetFactory>();

            _SelectedGroupObservable = new BehaviorSubject<bool>(false);

            var openAndSelectedGroup = Observable.CombineLatest(_SelectedGroupObservable, _Factory.IsOpen,( x , y)=> x && y);

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


            //
            // 必须打开数据集并且选择一个分组
            _RemoveGroupOperator = ReactiveCommand.Create(RemoveGroupCore, openAndSelectedGroup);


            //
            // 随时
            _GroupSelectorOperator = ReactiveCommand.Create<IBrushGroupAdapter>(OnSelectedBrushGroup);
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
                foreach(var brushContext in context.Context)
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
                _Factory.Remove(_SelectedGroup.Source);
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
            _SelectedGroup = group;
            _SelectedGroupObservable.OnNext(group is IBrushGroupAdapter);
            _Factory.FilterStream.OnNext(x => x.ParentId == group.Id);
        }

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