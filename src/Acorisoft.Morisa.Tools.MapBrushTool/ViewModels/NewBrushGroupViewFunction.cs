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
    public partial class NewBrushGroupViewFunction : DialogFunction
    {
        private string _GropName;
        private readonly IMapGroup _Parent;

        public NewBrushGroupViewFunction(IMapGroup parent)
        {
            _Parent = parent;
        }

        protected override object GetResultCore()
        {
            return Factory.CreateMapGroup(_GropName, _Parent);
        }

        protected override bool VerifyModelCore()
        {
            return !string.IsNullOrEmpty(_GropName);
        }

        public string Name
        {
            get => _GropName;
            set => Set(ref _GropName, value);
        }
    }
}