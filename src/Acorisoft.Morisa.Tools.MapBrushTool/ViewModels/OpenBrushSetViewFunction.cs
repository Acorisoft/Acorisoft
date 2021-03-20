using Acorisoft.Morisa.Core;
using Acorisoft.Morisa.Dialogs;
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
    public partial class OpenBrushSetViewFunction : DialogFunction
    {
        public OpenBrushSetViewFunction()
        {
        }


        private string _File;

        protected override object GetResultCore()
        {
            return _File;
        }

        protected override bool VerifyModelCore()
        {
            return !string.IsNullOrEmpty(_File);
        }

        public string File
        {
            get => _File;
            set
            {
                Set(ref _File, value);
            }
        }
    }
}