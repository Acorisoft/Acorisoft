using Acorisoft.Morisa.Map;
using Acorisoft.Morisa.ViewModels;
using DynamicData;
using DynamicData.Binding;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Joins;
using System.Reactive.Linq;
using System.Reactive.PlatformServices;
using System.Reactive.Subjects;
using System.Reactive.Threading;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Acorisoft.Morisa.Collections;
using Acorisoft.Morisa.Dialogs;
using Splat;
using System.Windows;
using DryIoc;
using Acorisoft.Morisa.ViewModels;
using System.Windows.Input;
using Acorisoft.Morisa.Core;

namespace Acorisoft.Morisa.Tools.ViewModels
{
    public class AppViewModel : ViewModelBase
    {
        private readonly ICommand                       _NewOperator;
        private readonly ICommand                       _OpenOperator;
        private readonly ICommand                       _SaveOperator;
        private readonly ICommand                       _AddBrushOperator;
        private readonly ICommand                       _RemoveBrushOperator;
        private readonly ICommand                       _RemoveThisGroupBrushOperator;
        private readonly ICommand                       _ClearAllBrushOperator;
        private readonly ICommand                       _AddGroupOperator;
        private readonly ICommand                       _RemoveGroupOperator;
        private readonly ICommand                       _RemoveThisGroupOperator;
        private readonly ICommand                       _ClearAllGroupOperator;
        private readonly IMapBrushSetFactory            _Factory;
        private readonly ReadOnlyObservableCollection<IMapBrush> _BrushCollection;
        private readonly ReadOnlyObservableCollection<IMapGroup> _GroupCollection;


        private readonly IDialogManager _DialogManager;

        public AppViewModel()
        {
            _DialogManager = Locator.Current.GetService<IDialogManager>();
            _NewOperator = ReactiveCommand.Create(OnNewOperator);
            _Factory = Locator.Current.GetService<IMapBrushSetFactory>();
            _BrushCollection = _Factory.BrushCollection;
            // _GroupCollection = _Factory.GroupCollection;

            _Factory.OnLoaded += OnRebuildGroupInformation;
        }

        protected async void OnNewOperator()
        {
            var session = await _DialogManager.Step<
                NewBrushSetDialogViewModel,
                NewBrushSetDialogStep2ViewModel,
                NewBrushSetDialogStep3ViewModel>(new GenerateContext<MapBrushSetInformation>());

            if(session.IsCompleted && 
               session.GetResult<GenerateContext<MapBrushSetInformation>>() is GenerateContext<MapBrushSetInformation> generateContext)
            {
                MessageBox.Show("完成");
            }
        }

        protected void OnRebuildGroupInformation(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand NewOperator => _NewOperator;

        /// <summary>
        /// 
        /// </summary>
        public ICommand OpenOperator => _OpenOperator;

        /// <summary>
        /// 
        /// </summary>
        public ICommand SaveOperator => _SaveOperator;

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
        public ICommand RemoveBrushOperator => _RemoveBrushOperator;

        /// <summary>
        /// 
        /// </summary>
        public ICommand RemoveGroupOperator => _RemoveGroupOperator;

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
