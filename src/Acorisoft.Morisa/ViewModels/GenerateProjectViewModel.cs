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
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Acorisoft.Morisa.ViewModels
{
    public partial class GenerateProjectViewModel : ViewModelBase, IResultable
    {
        private string _Name;
        private string _Summary;
        private string _Topic;
        private string _CoverFileName;
        private ImageObject _Cover;
        private readonly IMorisaFileManager _FileManager;


        public GenerateProjectViewModel(IMorisaFileManager fileMgr)
        {
            _FileManager = fileMgr;

            this.WhenAnyValue(x => x.CoverFileName)
                .Where(x => x != null)
                .Subscribe(x => _FileManager.WriteImage(x));

            _FileManager.Completed
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .Subscribe(x => Cover = x as ImageObject);
        }

        object IResultable.GetResult()
        {
            return new MorisaProjectInfo
            {
                Name = _Name ,
                Summary = _Summary ,
                Topic = _Topic ,
                Cover = _Cover
            };
        }

        bool IResultable.VerifyAccess()
        {
            return !string.IsNullOrEmpty(_Name);
        }

        public string Name
        {
            get => _Name;
            set => Set(ref _Name , value);
        }
        public string Summary
        {
            get => _Summary;
            set => Set(ref _Summary , value);
        }
        public string Topic
        {
            get => _Topic;
            set => Set(ref _Topic , value);
        }

        public ImageObject Cover
        {
            get => _Cover;
            set => Set(ref _Cover , value);
        }
        public string CoverFileName
        {
            get => _CoverFileName;
            set => Set(ref _CoverFileName , value);
        }
    }
}