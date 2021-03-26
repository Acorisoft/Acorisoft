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
using Acorisoft.Morisa.Tools.Models;

namespace Acorisoft.Morisa.Tools.ViewModels
{
    public class AppViewModel : ViewModelBase
    {
        private readonly IBrushSetFactory _Factory;
        private readonly ICommand _OpenOperator;
        private readonly ICommand _NewOperator;

        public AppViewModel()
        {
            _Factory = GetService<IBrushSetFactory>();

            //
            // declare commands 
            _OpenOperator = ReactiveCommand.Create(OpenOperatorCore);
            _NewOperator = ReactiveCommand.Create(NewOperatorCore);
        }

        protected async void OpenOperatorCore()
        {
            var session = await Dialog<OpenBrushSetDialogViewFunction>();

            //
            // 打开对话框
            if(session.IsCompleted && session.GetResult<ILoadContext>() is ILoadContext context)
            {
                _Factory.Load(context);
            }
        }
        protected async void NewOperatorCore()
        {
            var session = await Step<SaveBrushSetStepViewFunction,SaveBrushSetStep2ViewFunction>(
                new BrushSetGenerateContext());

            //
            // 打开对话框
            if (session.IsCompleted && session.GetResult<BrushSetGenerateContext>() is BrushSetGenerateContext context)
            {
                _Factory.Save(context);
            }
        }


        public ICommand OpenOperator => _OpenOperator;
        public ICommand NewOperator => _NewOperator;
    }
}
