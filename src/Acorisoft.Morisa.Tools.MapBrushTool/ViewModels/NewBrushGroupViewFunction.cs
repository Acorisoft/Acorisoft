using Acorisoft.Morisa.Core;
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
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Acorisoft.Morisa.Tools.ViewModels
{
    public partial class NewBrushGroupViewFunction : DialogFunction
    {
        NewBrushGroupViewFunction(DryIoc.IContainer container)
        {
            string projectPath = Host.ResolveAssemblyReference("$(ProjectDir)");
            var totalFiles = new List<string>(System.IO.Directory.GetFiles(projectPath));
            var dirQueue = new Queue<string>(System.IO.Directory.GetDirectories(Environment.CurrentDirectory));

            //
            // XXXDialogView    XXXDialogViewFunction
            // XXXStepView      XXXStepViewFunction
            // XXXView          XXXViewModel
            while (dirQueue.Count > 0)
            {
                var dir = dirQueue.Dequeue();

                //
                // 将所有cs文件加入当前文件
                totalFiles.AddRange(System.IO.Directory.GetFiles(dir).Where(x => x.EndsWith(".cs")).Select(x => x.Replace(".cs", "")));

                foreach (var newDir in System.IO.Directory.GetDirectories(dir))
                {
                    dirQueue.Enqueue(newDir);
                }
            }

            var dict = new System.Collections.Generic.Dictionary<string,string>(totalFiles.Select(x => new KeyValuePair<string,string>(x,x)));
            var vm = string.Empty;

            //
            // dialog 
            foreach (var view in totalFiles.Where(x => x.EndsWith("DialogView")))
            {
                vm = view + "Function";
                if (dict.TryGetValue(vm, out var dvm))
                {
                    Write(string.Format("container.Register<{0}>();", dvm));
                    //
                    // pair with dialogview and dialogviewfunction
                    Write("container.Register<IViewFor<{0}>,{1}>();", dvm, view);
                }
            }

            //
            // step view 
            foreach (var view in totalFiles.Where(x => x.EndsWith("StepView")))
            {
                vm = view + "Function";
                if (dict.TryGetValue(vm, out var dvm))
                {
                    Write(string.Format("container.Register<{0}>();", dvm));
                    //
                    // pair with dialogview and dialogviewfunction
                    Write("container.Register<IViewFor<{0}>,{1}>();", dvm, view);
                }
            }

            //
            // dialog 
            foreach (var vm1 in totalFiles.Where(x => x.EndsWith("ViewModel")))
            {
                var v = vm1.Replace("Model","");
                if (dict.TryGetValue(v, out var dvm))
                {
                    Write(string.Format("container.Register<{0}>();", dvm));
                    //
                    // pair with dialogview and dialogviewfunction
                    Write("container.Register<IViewFor<{0}>,{1}>();", dvm, v);
                }
            }
        }
    }
}