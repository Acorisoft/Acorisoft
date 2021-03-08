using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Tests.Morisa
{
    [TestClass]
    public class MorisaProjectManagerTest
    {
        [TestMethod]
        public void OpenProjectTest()
        {
            var mpMgr = new MorisaProjectManager();
            mpMgr.Subscribe(x =>
            {
                Assert.IsTrue(x != null);
            });
            mpMgr.Load(Environment.CurrentDirectory);
        }
    }
}
