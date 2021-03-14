using Acorisoft.Morisa;
using Acorisoft.Morisa.Emotions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Documents.Emotions
{
    [TestClass]
    public class EmotionMechanismTest
    {
        [TestMethod]
        public void Emotion_Add_Test()
        {
            var mechanism = new EmotionMechanism();
            ((INotifyCollectionChanged)mechanism.Collection).CollectionChanged += (sender, e) =>
            {
                Assert.IsTrue(e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Reset);
            };
            var compositionSet = new CompositionSet(@"D:\Repo\Acorisoft\Bin\test");
            mechanism.Input.OnNext(compositionSet);
            mechanism.Input.OnCompleted();
            mechanism.Add(CompositionElementFactory.CreateMottoEmotion());
        }

        [TestMethod]
        public void Emotion_Load_Test()
        {
            var mechanism = new EmotionMechanism();
            var compositionSet = new CompositionSet(@"D:\Repo\Acorisoft\Bin\test");
            mechanism.Input.OnNext(compositionSet);
            mechanism.Input.OnCompleted();
            Assert.IsTrue(mechanism.Collection.Count > 0);
        }
    }
}
