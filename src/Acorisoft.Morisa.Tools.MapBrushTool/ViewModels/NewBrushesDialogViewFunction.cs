using Acorisoft.Morisa.Core;
using Acorisoft.Morisa.Dialogs;
using Acorisoft.Morisa.Tools.Models;
using Acorisoft.Morisa.ViewModels;
using DynamicData.Binding;
using GongSolutions.Wpf.DragDrop;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    public partial class NewBrushesDialogViewFunction : DialogFunction
    {
        private BrushesGenerateContext _Context;
        private string _Folder;

        public NewBrushesDialogViewFunction()
        {
            _Context = new BrushesGenerateContext();
        }

        public string Folder
        {
            get => _Folder;
            set
            {
                if (Set(ref _Folder , value))
                {
                    //
                    //
                    _Context.Brushes.Clear();

                    //
                    //
                    var files = Directory.GetFiles(value)
                                         .Where(x =>
                                         {
                                             var fi = new FileInfo(x);
                                             return fi.Extension == ".png" && fi.Extension == ".jpg" && fi.Extension == ".bmp";
                                         })
                                         .Select(x => new GenerateContext<Brush>();


                }
            }
        }
    }
}