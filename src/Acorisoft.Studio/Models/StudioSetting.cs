using System;
using Acorisoft.Extensions.Platforms.ComponentModel;
using Acorisoft.Extensions.Platforms.Windows.Models;
using Acorisoft.Studio.ProjectSystems;

namespace Acorisoft.Studio.Models
{
    public class StudioSetting : ApplicationSetting
    {
        /// <summary>
        /// 最近的
        /// </summary>
        public ComposeProject RecentProject { get; set; }
    }

    public class StudioSettingWrapper : ObservableObject
    {
        public StudioSettingWrapper(StudioSetting source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public StudioSetting Source { get; }

        /// <summary>
        /// 是否开启应用程序关闭提示。true 表示开启，false表示关闭。
        /// </summary>
        public bool EnableWindowCloseQuery
        {
            get => Source.EnableWindowCloseQuery;
            set
            {
                Source.EnableWindowCloseQuery = value;
                RaiseUpdated();
            }
        }

        /// <summary>
        /// 是否开启单例应用程序。true 表示开启，false表示关闭。
        /// </summary>
        public bool EnableSingleApplication
        {
            get => Source.EnableSingleApplication;
            set
            {
                Source.EnableSingleApplication = value;
                RaiseUpdated();
            }
        }


        /// <summary>
        /// 最近的
        /// </summary>
        public ComposeProject RecentProject
        {
            get => Source.RecentProject;
            set
            {
                Source.RecentProject = value;
                RaiseUpdated();
            }
        }
    }
}