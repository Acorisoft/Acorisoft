using Acorisoft.Morisa;
using Acorisoft.Morisa.Core;
using Acorisoft.Morisa.Persistants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Documents.Emotions
{
    [TestClass]
    public class CompositionSetManagerTest
    {
        [TestMethod]
        public void CompositionSetManager_Load_ICompositionSetInfo_Test()
        {
            var csMgr = new CompositionSetManager();
            
            csMgr.Changed += (sender, e) =>
            {
                Assert.IsNotNull(e.NewValue);
            };
            csMgr.Opened += (sender, e) =>
            {
                Assert.IsNotNull(e.NewValue);
            };
            csMgr.Load(new CompositionSetInfo
            {
                Name = "Untitled",
                Summary = CompositionSet.LoremIpsum,
                Directory = @"D:\Repo\Acorisoft\Bin\test",
                FileName = @"D:\Repo\Acorisoft\Bin\test\Main.Morisa-Project",
                Tags = new List<string> { "test", "cs", "character" },
                Topic = CompositionSet.LoremIpsum,
                Cover = new InDatabaseResource
                {
                    FileName = @"test.png"
                }
            }); ;
        }

        [TestMethod]
        public void CompositionSetManager_Load_String_Test()
        {
            var csMgr = new CompositionSetManager();
            csMgr.Changed += (sender, e) =>
            {
                Assert.IsNotNull(e.NewValue);
            };
            csMgr.Opened += (sender, e) =>
            {
                Assert.IsNotNull(e.NewValue);
            };
            csMgr.Load(@"D:\Repo\Acorisoft\Bin\test");
            csMgr.Load(@"D:\Repo\Acorisoft\Bin\test\Main.Morisa-Project");
        }

        [TestMethod]
        public void CompositionSetManager_Load_ICompositionSetStore_Test()
        {
            var csMgr = new CompositionSetManager();
            csMgr.Changed += (sender, e) =>
            {
                Assert.IsNotNull(e.NewValue);
            };
            csMgr.Opened += (sender, e) =>
            {
                Assert.IsNotNull(e.NewValue);
            };
            csMgr.Load(new CompositionSetStore
            {
                Name = "Untitled",
                Directory = @"D:\Repo\Acorisoft\Bin\test",
                FileName = @"D:\Repo\Acorisoft\Bin\test\Main.Morisa-Project",
            });
        }
    }
}
