using Acorisoft.Morisa.Dialogs;
using Acorisoft.Morisa.Map;
using Acorisoft.Morisa.ViewModels;
using DynamicData.Binding;
using GongSolutions.Wpf.DragDrop;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Acorisoft.Morisa.Tools.ViewModels
{
    public partial class NewBrushSetDialogStep3ViewModel : StepFunction<GenerateContext<MapBrushSetInformation>>
    {

        public NewBrushSetDialogStep3ViewModel()
        {
        }

        private string _File;

        protected override bool VerifyModelCore()
        {
            return !string.IsNullOrEmpty(_File);
        }

        public string File
        {
            get => _File;
            set
            {
                Context.FileName = value;
                Context.Directory = Directory.GetParent(value).FullName;
                Set(ref _File, value);
            }
        }
    }
}