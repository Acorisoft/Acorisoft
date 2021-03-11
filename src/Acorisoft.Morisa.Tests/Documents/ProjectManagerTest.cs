using Acorisoft.Morisa.ViewModels;
using LiteDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Threading;
using System.Reactive.Disposables;
using DynamicData.Binding;
using ReactiveUI;

namespace Acorisoft.Morisa
{
    [TestClass]
    public class ProjectManagerTest
    {
        [TestMethod]
        public void LoadOrCreateProject_string_Crate_Test()
        {
            var mgr = new MorisaProjectManager();
            mgr.Project.Subscribe(x => Assert.IsNotNull(x));
            mgr.LoadOrCreateProject("E:\\Workbook\\Main2.Morisa-Project");
        }

        [TestMethod]
        public void LoadOrCreateProject_string_Load_Test()
        {
            var mgr = new MorisaProjectManager();
            mgr.Project.Subscribe(x => Assert.IsNotNull(x));
            mgr.ProjectInfo.Subscribe(x => Assert.IsNotNull(x));
            mgr.LoadOrCreateProject("E:\\Workbook\\Main1.Morisa-Project");
        }

        [TestMethod]
        public void LoadOrCreateProject_ProjectInfo_Create_Test()
        {
            var mgr = new MorisaProjectManager();
            mgr.Project.Subscribe(x => Assert.IsNotNull(x));
            mgr.ProjectInfo.Subscribe(x => Assert.IsNotNull(x));
            mgr.LoadOrCreateProject((IMorisaProjectInfo)new MorisaProjectInfo
            {
                FileName = "E:\\Workbook\\Main.Morisa-Project" ,
                Directory = "E:\\Workbook\\"
            });
        }
        [TestMethod]
        public void LoadOrCreateProject_ProjectTargetInfo_Create_Test()
        {
            var mgr = new MorisaProjectManager();
            mgr.Project.Subscribe(x => Assert.IsNotNull(x));
            mgr.ProjectInfo.Subscribe(x => Assert.IsNotNull(x));
            mgr.LoadOrCreateProject((IMorisaProjectTargetInfo)new MorisaProjectInfo
            {
                FileName = "E:\\Workbook\\Main3.Morisa-Project" ,
                Directory = "E:\\Workbook\\"
            });
        }
    }
}
