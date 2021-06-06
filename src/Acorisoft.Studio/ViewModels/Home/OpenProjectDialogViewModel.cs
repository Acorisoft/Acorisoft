using System;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.Core;
using Acorisoft.Studio.Properties;
using ReactiveUI;

namespace Acorisoft.Studio.ViewModels
{
    public class OpenProjectDialogViewModel : DialogViewModelBase, INewItemInfo<ComposeProject>
    {
        private string _name;
        private string _path;

        public OpenProjectDialogViewModel() : base()
        {
            // var observable = this.WhenAnyValue(x => x.Name, (name) => !string.IsNullOrEmpty(name));
            Item = new ComposeProject();
        }

        public sealed override bool CanCancel()
        {
            return true;
        }

        public sealed override bool VerifyAccess()
        {
            return !string.IsNullOrEmpty(_path);
        }

        public sealed override string UrlPathSegment => SR.Home_NewProject;

        public Guid Id { get; set; }

        /// <summary>
        /// 获取或设置新的项目名称。
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                Item.Name = value;
                Set(ref _name, value);
            }
        }

        public ComposeProject Item { get; set; }

        /// <summary>
        /// 获取或设置新的项目存放路径。
        /// </summary>
        public string Path
        {
            get => _path;
            set
            {
                Item.Path = value;
                Set(ref _path, value);
            }
        }
    }
}