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

namespace Acorisoft.Morisa.ViewModels
{
    public partial class GenerateCompositionSetViewModel : DialogFunction
    {
        private readonly ICompositionSetInfo _Info;

        public GenerateCompositionSetViewModel()
        {
            _Info = new CompositionSetInfo();
        }

        protected override object GetResultCore()
        {
            return _Info;
        }

        protected override bool VerifyModelCore()
        {
            return !string.IsNullOrEmpty(Name);
        }

        public string Directory
        {
            get => _Info.Directory;
            set
            {
                _Info.Directory = value;
                RaiseUpdated(nameof(Directory));
            }
        }
        public string FileName
        {
            get => _Info.FileName;
            set
            {
                _Info.FileName = value;
                RaiseUpdated(nameof(FileName));
            }
        }
        public string Name
        {
            get => _Info.Name;
            set
            {
                _Info.Name = value;
                RaiseUpdated(nameof(Name));
            }
        }
        public string Summary
        {
            get => _Info.Summary;
            set
            {
                _Info.Summary = value;
                RaiseUpdated(nameof(Summary));
            }
        }
        public string Topic
        {
            get => _Info.Topic;
            set
            {
                _Info.Topic = value;
                RaiseUpdated(nameof(Topic));
            }
        }
        public List<string> Tags
        {
            get => _Info.Tags;
            set
            {
                _Info.Tags = value;
                RaiseUpdated(nameof(Tags));
            }
        }
        public Resource Cover
        {
            get => _Info.Cover;
            set
            {
                _Info.Cover = value;
                RaiseUpdated(nameof(Cover));
            }
        }
    }
}