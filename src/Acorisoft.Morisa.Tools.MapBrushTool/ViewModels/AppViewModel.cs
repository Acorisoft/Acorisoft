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
using System.Windows.Input;
using Acorisoft.Morisa.Core;

namespace Acorisoft.Morisa.Tools.ViewModels
{
    public class AppViewModel : ViewModelBase
    {
        private readonly ICommand                       _NewOperator;
        private readonly ICommand                       _OpenOperator;
        private readonly ICommand                       _SaveOperator;
        private readonly IMapBrushSetFactory            _Factory;
        private readonly ReadOnlyObservableCollection<IMapBrush> _BrushCollection;
        private readonly ReadOnlyObservableCollection<IMapGroup> _GroupCollection;

        public AppViewModel()
        {
            _NewOperator = ReactiveCommand.Create(OnNewOperator);
            _OpenOperator = ReactiveCommand.Create(OnOpenOperator);
            _Factory = Locator.Current.GetService<IMapBrushSetFactory>();
            _Factory.OnLoaded += OnRebuildGroupInformation;
            _BrushCollection = _Factory.BrushCollection;

        }

        protected async void OnNewOperator()
        {
            var session = await DialogManager.Step<
                NewBrushSetDialogViewModel,
                NewBrushSetDialogStep2ViewModel,
                NewBrushSetDialogStep3ViewModel>(new GenerateContext<MapBrushSetInformation>
                {
                    Context = new MapBrushSetInformation()
                });

            if(session.IsCompleted && 
               session.GetResult<GenerateContext<MapBrushSetInformation>>() is GenerateContext<MapBrushSetInformation> generateContext)
            {
                _Factory.Generate(generateContext);
            }
        }

        protected async void OnOpenOperator()
        {
            var session = await DialogManager.Dialog<OpenBrushSetViewFunction>();
            if(session.IsCompleted && session.GetResult<string>() is string fileName)
            {
                _Factory.Load(new StoreContext { FileName = fileName });
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


    }
}
