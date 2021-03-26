using Acorisoft.Morisa.Core;
using Acorisoft.Morisa.Dialogs;
using Acorisoft.Morisa.Map;
using Acorisoft.Morisa.Tools.Models;
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
    public partial class NewBrushDialogViewFunction : DialogFunction
    {

        private string _fileName;
        private BrushGenerateContext _Context;

        public NewBrushDialogViewFunction()
        {
            _Context = new BrushGenerateContext();
        }

        protected override bool VerifyModelCore()
        {
            return !string.IsNullOrEmpty(_fileName);
        }

        protected override object GetResultCore()
        {
            return _Context;
        }

        public string FileName
        {
            get => _fileName;
            set
            {
                if(Set(ref _fileName, value))
                {
                    _Context.FileName = value;
                }
            }
        }
    }
}