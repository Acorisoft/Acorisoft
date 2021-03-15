using Acorisoft.Morisa.Emotions;
using Acorisoft.Morisa.ViewModels;
using DynamicData.Binding;
using GongSolutions.Wpf.DragDrop;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Acorisoft.Morisa.ViewModels
{
    public partial class EmotionViewModel : ViewModelBase
    {
        private readonly IEmotionMechanism _Mechanism;
        private readonly ICommand _AddOperator;
        private readonly ICommand _ClearOperator;
        private readonly ICommand _RemoveOperator;

        public EmotionViewModel()
        {
            _Mechanism = Locator.Current.GetService< IEmotionMechanism>();
            _AddOperator = ReactiveCommand.Create(async ()=>
            {
                var count = new Random().Next(0,12);
                var elements = new[]
                {
                    CompositionElementFactory.CreateMottoEmotion(),
                    CompositionElementFactory.CreateMottoEmotion("当爱情萌芽的时候，只需要悄悄地给点希望就可以了","新房昭之")
                };
                var element = elements[count % 2];
                element.Creation = DateTime.Now;
                _Mechanism.Add(element);
            });
            _RemoveOperator = ReactiveCommand.Create<IEmotionElement>(async x =>
            {
                if(x is IEmotionElement)
                {
                    _Mechanism.Remove(x);
                }
            });
            _ClearOperator = ReactiveCommand.Create(() =>
            {
                _Mechanism.Clear();
            });
        }

        public ICommand AddOperator => _AddOperator;
        public ICommand RemoveOperator => _RemoveOperator;
        public ICommand ClearOperator => _ClearOperator;
        public ReadOnlyObservableCollection<IEmotionElement> Collection => _Mechanism.Collection;
        public int Page
        {
            get => _Mechanism.Page;
            set => _Mechanism.Page = value;
        }
        public int PageSize
        {
            get => _Mechanism.PageSize;
            set => _Mechanism.PageSize = value;
        }
    }
}