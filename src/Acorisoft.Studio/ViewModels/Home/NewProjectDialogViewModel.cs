using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI.Validation.Extensions;
using ReactiveUI;
using Acorisoft.Studio.ProjectSystem;
using Acorisoft.Studio.Core;

namespace Acorisoft.Studio.ViewModels
{
    public class NewProjectDialogViewModel : DialogViewModelBase, INewProjectInfo, INewItemInfo<IComposeSetProperty>
    {
        private string _name;
        private string _path;

        public NewProjectDialogViewModel() : base()
        {
            // var observable = this.WhenAnyValue(x => x.Name, (name) => !string.IsNullOrEmpty(name));
            Item = new ComposeSetProperty();
        }

        public sealed override bool CanCancel()
        {
            return true;
        }

        public sealed override bool VerifyAccess()
        {
            return !string.IsNullOrEmpty(_name) && !string.IsNullOrEmpty(_path);
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

        public IComposeSetProperty Item { get; set; }

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