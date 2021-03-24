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
    public partial class OpenBrushSetDialogViewFunction : DialogFunction
    {
  

        private string _fileName;
        private ILoadContext _context;

        public OpenBrushSetDialogViewFunction()
        {
        }
        protected override bool VerifyModelCore()
        {
            return !string.IsNullOrEmpty(_context?.FileName);
        }

        protected override object GetResultCore()
        {
            return _context;
        }

        public string FileName
        {
            get => _fileName;
            set
            {
                _context = new StringLoadContext
                {
                    FileName = value
                };
                Set(ref _fileName, value);
            }
        }
    }
}