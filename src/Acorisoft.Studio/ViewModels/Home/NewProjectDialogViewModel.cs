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

namespace Acorisoft.Studio.ViewModels
{
    public class NewProjectDialogViewModel : DialogViewModelBase, INewProjectInfo
    {
        private string _name;
        private string _path;

        public NewProjectDialogViewModel() : base()
        {
            var observable = this.WhenAnyValue(x => x.Name, (name) => !string.IsNullOrEmpty(name));
        }

        public override sealed bool CanCancel()
        {
            return true;
        }

        public override sealed bool VerifyAccess()
        {
            return !string.IsNullOrEmpty(_name) && !string.IsNullOrEmpty(_path);
        }

        public override sealed string UrlPathSegment => SR.Home_NewProject;

        /// <summary>
        /// 获取或设置新的项目名称。
        /// </summary>
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        /// <summary>
        /// 获取或设置新的项目存放路径。
        /// </summary>
        public string Path
        {
            get => _path;
            set => Set(ref _path, value);
        }
    }
}
