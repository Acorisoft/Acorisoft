using Acorisoft.Morisa.Dialogs;
using Acorisoft.Morisa.Map;
using Acorisoft.Morisa.ViewModels;
using DynamicData.Binding;
using GongSolutions.Wpf.DragDrop;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Acorisoft.Morisa.Tools.ViewModels
{
    public partial class NewBrushSetDialogStep2ViewModel : StepFunction<GenerateContext<MapBrushSetInformation>>
    {
        private string _tag;

        public NewBrushSetDialogStep2ViewModel()
        {
            Tags = new ObservableCollectionExtended<string>();
            Tags.CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (string item in e.NewItems)
                {
                    Context.Context.Tags.Add(item);
                }
            }
            else if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (string item in e.OldItems)
                {
                    Context.Context.Tags.Remove(item);
                }
            }
            else
            {
                Context.Context.Tags.Clear();
            }
        }

        protected override bool VerifyModelCore()
        {
            var context = Context.Context;
            return !string.IsNullOrEmpty(context.Authors);
        }

        public string Tag
        {
            get => _tag;
            set
            {
                Set(ref _tag, value);
            }
        }

        public string Authors
        {
            get => Context.Context.Authors;
            set
            {
                Context.Context.Authors = value;
                RaiseUpdated(nameof(Authors));
            }
        }

        public ObservableCollectionExtended<string> Tags
        {
            get;
        }
    }
}