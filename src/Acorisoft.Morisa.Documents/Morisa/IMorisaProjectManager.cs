using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public interface IMorisaProjectManager
    {
        /// <summary>
        /// 创建或者打开一个项目。
        /// </summary>
        /// <param name="target">指定要打开的项目类型。</param>
        void LoadOrCreateProject(string target);

        /// <summary>
        /// 创建或者打开一个项目。
        /// </summary>
        /// <param name="target">指定要打开的项目类型。</param>
        void LoadOrCreateProject(IMorisaProjectInfo target);

        /// <summary>
        /// 获取一个可观测的项目信息流。
        /// </summary>
        IObservable<IMorisaProjectInfo> ProjectInfo { get; }

        /// <summary>
        /// 获取一个可观测的项目流。
        /// </summary>
        IObservable<IMorisaProject> Project { get; }
    }
}
