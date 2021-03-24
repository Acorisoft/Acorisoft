using Acorisoft.Morisa.Dialogs;
using Acorisoft.Morisa.Map;
using Acorisoft.Morisa.Tools.Models;
using Acorisoft.Morisa.Tools.ViewModels;
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
    public partial class SaveBrushSetStep2ViewFunction : StepFunction<BrushSetGenerateContext>
    {
        private string _fileName;

        protected override bool VerifyModelCore()
        {
            return !string.IsNullOrEmpty(Context.FileName) && !string.IsNullOrEmpty(Context.Property.Name);
        }

        public string FileName
        {
            get => _fileName;
            set
            {
                Context.FileName = value;
                Set(ref _fileName, value);
            }
        }
    }
}