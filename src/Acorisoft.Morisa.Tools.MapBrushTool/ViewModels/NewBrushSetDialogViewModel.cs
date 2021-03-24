﻿using Acorisoft.Morisa.Dialogs;
using Acorisoft.Morisa.Map;
using Acorisoft.Morisa.ViewModels;
using DynamicData.Binding;
using GongSolutions.Wpf.DragDrop;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Acorisoft.Morisa.Tools.ViewModels
{
    public partial class NewBrushSetDialogViewModel : StepFunction<GenerateContext<MapBrushSetInformation>>
    {
        public NewBrushSetDialogViewModel()
        {
        }

        protected override bool VerifyModelCore()
        {
            var context = Context.Context;
            return !string.IsNullOrEmpty(context.Name);
        }

        public string Name
        {
            get => Context.Context.Name;
            set
            {
                Context.Context.Name = value;
                RaiseUpdated(nameof(Name));
            }
        }

        public string Summary
        {
            get => Context.Context.Summary;
            set
            {
                Context.Context.Summary = value;
                RaiseUpdated(nameof(Summary));
            }
        }
    }
}