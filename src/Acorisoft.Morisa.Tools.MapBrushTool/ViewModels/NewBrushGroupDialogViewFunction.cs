using Acorisoft.Morisa.Core;
using Acorisoft.Morisa.Dialogs;
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
    public partial class NewBrushGroupDialogViewFunction : DialogFunction
    {
        private string _name;
        private string _summary;
        private IBrushGroup _group;

        public NewBrushGroupDialogViewFunction()
        {
            _group = new BrushGroup();
        }


        protected override bool VerifyModelCore()
        {
            return !string.IsNullOrEmpty(_name);
        }

        protected override object GetResultCore()
        {
            return _group;
        }

        public string Name
        {
            get => _name;
            set
            {
                _group.Name = value;
                Set(ref _name, value);
            }
        }
    }
}