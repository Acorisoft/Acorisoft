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
using Acorisoft.Morisa.IO;

namespace Acorisoft.Morisa
{
    [TestClass]
    public class FileManagerTest
    {
        [TestMethod]
        public void UploadFileTest()
        {
            var ProjectManager = new MorisaProjectManager();
            var FileManager = new MorisaFileManager();

            ProjectManager.Project.Subscribe(x =>
            {
                FileManager.Project.OnNext(x);
                FileManager.Project.OnCompleted();
            });

            ProjectManager.LoadOrCreateProject((IMorisaProjectTargetInfo)new MorisaProjectInfo
            {
                FileName = @"E:\Workbook\Main4.Morisa-Project" ,
                Directory = @"E:\Workbook"
            }) ;
            FileManager.IgnoreFileDuplicate = true;
            FileManager.WriteImage(@"C:\Users\zhongxin013\Documents\HZSG\Assets\ico_512x512.ico");
            FileManager.Completed.Subscribe(x => Assert.IsNotNull(x));
        }
    }
}
