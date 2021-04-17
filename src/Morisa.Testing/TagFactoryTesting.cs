using Acorisoft.Morisa.Composition;
using Acorisoft.Morisa.EventBus;
using Acorisoft.Morisa.Tags;
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
using Acorisoft.Morisa.Composition;
using Acorisoft.Morisa.Core;
using Acorisoft.Morisa.EventBus;
using Acorisoft.Morisa.Properties;
using DynamicData;
using LiteDB;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BindableTagBridgeCollection = System.Collections.ObjectModel.ReadOnlyObservableCollection<Acorisoft.Morisa.Tags.ITagBridge>;

namespace Acorisoft.Morisa
{
    [TestClass]
    public class TagFactoryTesting
    {
        private readonly IContainer _Container;

        public TagFactoryTesting()
        {
            _Container = new Container(Rules.Default.WithTrackingDisposableTransients());
            _Container.UseDryIoc().UseLog().UseMorisa();
        }

        [TestMethod]
        public void GetParent_Test()
        {
            var factory = new SourceCache<ITag,Guid>(x => x.Id);
            ReadOnlyObservableCollection<ITagBridge> ss;

            factory.Connect()
                   .TransformToTree(x => x.ParentId)
                   .Transform(x => (ITagBridge)new TagBridge(x, (o) => { }))
                   .Bind(out ss)
                   .Subscribe();
            var p1 = new Tag
            {
                Color = "#007ACC",
                Name  = $"Tag_Parent1",
                Id = Guid.NewGuid(),
            };
            var p2 = new Tag
            {
                Color = "#007ACC",
                Name  = $"Tag_Parent2",
                Id = Guid.NewGuid(),
            };
            var p3 = new Tag
            {
                Color = "#007ACC",
                Name  = $"Tag_Parent3",
                Id = Guid.NewGuid(),
            };
            factory.AddOrUpdate(p1);
            factory.AddOrUpdate(p2);
            factory.AddOrUpdate(p3);
            for (int i = 0; i < 100; i++)
            {
                factory.AddOrUpdate(new Tag
                {
                    Color = "#007ACC",
                    Name = $"Tag{i}",
                    Id = Guid.NewGuid(),
                    ParentId = p1.Id
                });
            }

            for (int i = 100; i < 200; i++)
            {
                factory.AddOrUpdate(new Tag
                {
                    Color = "#007ACC",
                    Name = $"Tag{i}",
                    Id = Guid.NewGuid(),
                    ParentId = p2.Id
                });
            }
            for (int i = 200; i < 300; i++)
            {
                factory.AddOrUpdate(new Tag
                {
                    Color = "#007ACC",
                    Name = $"Tag{i}",
                    Id = Guid.NewGuid(),
                    ParentId = p3.Id
                });
            }

            //
            // set all p3 node's child to root
            foreach(var child in factory.Items.Where(x => x.ParentId == p3.Id).ToArray())
            {
                child.ParentId = Guid.Empty;
                factory.AddOrUpdate(child);
            }
        }

        [TestMethod]
        public void Add_Test()
        {
            var context = new LoadContext
            {
                FileName = @"D:\Test.MSet",
                Directory = @"D:\",
                Name = "Acoris"
            };

            var factory = _Container.Resolve<ITagFactory>();
            var mgr = _Container.Resolve<ICompositionSetManager>();
            mgr.Opened += csc => Assert.IsTrue(mgr.Activating is not null && csc is not null && mgr.Opening.Count > 0);
            mgr.Closed += csc => Assert.IsTrue(csc is not null && mgr.Opening.Count < 1);
            mgr.Load(context);

            var p1 = new Tag
            {
                Color = "#007ACC",
                Name  = $"Tag_Parent1",
                Id = Guid.NewGuid(),
            };
            var p2 = new Tag
            {
                Color = "#007ACC",
                Name  = $"Tag_Parent2",
                Id = Guid.NewGuid(),
            };
            var p3 = new Tag
            {
                Color = "#007ACC",
                Name  = $"Tag_Parent3",
                Id = Guid.NewGuid(),
            };

            for (int i = 0; i < 1000; i++)
            {
                factory.Add(new Tag
                {
                    Color = "#007ACC",
                    Name = $"Tag{i}",
                    Id = Guid.NewGuid(),
                    ParentId = p1.Id
                });
            }

            for (int i = 1000; i < 2000; i++)
            {
                factory.Add(new Tag
                {
                    Color = "#007ACC",
                    Name = $"Tag{i}",
                    Id = Guid.NewGuid(),
                    ParentId = p2.Id
                });
            }
            for (int i = 2000; i < 3000; i++)
            {
                factory.Add(new Tag
                {
                    Color = "#007ACC",
                    Name = $"Tag{i}",
                    Id = Guid.NewGuid(),
                    ParentId = p3.Id
                });
            }
            Assert.IsTrue(factory.Collection.Count > 2998);
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

            var factory = _Container.Resolve<ITagFactory>();
            var mgr = _Container.Resolve<ICompositionSetManager>();
            mgr.Opened += csc => Assert.IsTrue(mgr.Activating is not null && csc is not null && mgr.Opening.Count > 0);
            mgr.Closed += csc => Assert.IsTrue(csc is not null && mgr.Opening.Count < 1);
            mgr.Load(context);
            Assert.IsTrue(factory.Collection.Count > 2998);
        }
    }
}
