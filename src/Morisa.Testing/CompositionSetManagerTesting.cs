using Acorisoft.Morisa.Composition;
using Acorisoft.Morisa.EventBus;
using DryIoc;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    class TestNotification : INotificationHandler<CompositionSetOpeningInstruction>
    {
        public async Task Handle(CompositionSetOpeningInstruction notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine("组件已更新");
            Assert.IsTrue(notification is not null && notification.Context is not null);
        }
    }

    [TestClass]
    public class CompositionSetManagerTesting
    {
        private readonly IContainer _Container;

        public CompositionSetManagerTesting()
        {
            _Container = new Container(Rules.Default.WithTrackingDisposableTransients());
            _Container.RegisterInstanceMany<INotificationHandler<CompositionSetOpeningInstruction>>(new TestNotification());
            _Container.UseDryIoc().UseLog().UseMorisa();
        }

        [TestMethod]
        public void Generate_Test()
        {
            var context = new SaveContext<CompositionSetProperty>()
            {
                Property = new CompositionSetProperty
                {
                    Author = "Acoris",
                    Name = "Acoris"
                },
                FileName = @"D:\Test.MSet",
                Directory = @"D:\",
                Name = "Acoris"
            };


            var mgr = _Container.Resolve<ICompositionSetManager>();
            mgr.Opened += csc => Assert.IsTrue(mgr.Activating is not null && csc is not null && mgr.Opening.Count > 0);
            mgr.Closed += csc => Assert.IsTrue(csc is not null && mgr.Opening.Count < 1);
            mgr.OpenFailed += (o, e) =>
            {

            };

            mgr.Generate(context);
            mgr.Close();
            mgr.Dispose();
        }

        [TestMethod]
        public void Load_Test()
        {
            var context = new LoadContext
            {
                FileName = @"D:\Test.MSet",
                Directory = @"D:\",
                Name = "Acoris"
            };

            var mgr = _Container.Resolve<ICompositionSetManager>();
            mgr.Opened += csc => Assert.IsTrue(mgr.Activating is not null && csc is not null && mgr.Opening.Count > 0);
            mgr.Closed += csc => Assert.IsTrue(csc is not null && mgr.Opening.Count < 1);
            mgr.Load(context);
            mgr.Close();
        }

        [TestMethod]
        public void Switch_Test()
        {
            var context = new LoadContext
            {
                FileName = @"D:\Test.MSet",
                Directory = @"D:\",
                Name = "Acoris"
            }; 
            
            var context1 = new LoadContext
            {
                FileName = @"D:\Test1.MSet",
                Directory = @"D:\",
                Name = "Acoris"
            };

            var mgr = _Container.Resolve<ICompositionSetManager>();
            mgr.Opened += csc => Assert.IsTrue(mgr.Activating is not null && csc is not null && mgr.Opening.Count > 0);
            mgr.Closed += csc => Assert.IsTrue(csc is not null);
            mgr.Load(context);
            mgr.Load(context1);
            mgr.Switch(mgr.Opening[0]);
            mgr.Close();
            mgr.Close();
        }
    }
}
