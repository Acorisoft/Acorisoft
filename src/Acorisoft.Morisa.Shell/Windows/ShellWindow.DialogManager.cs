using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DryIoc;
using Splat;
using Splat.DryIoc;
using System.Diagnostics;
using Acorisoft.Dialogs;
using Acorisoft.ViewModels;

namespace Acorisoft.Windows
{
    partial class ShellWindow
    {
        private readonly IDialogManager _DialogManager;

        public ShellWindow()
        {
            _DialogManager = Locator.Current.GetService<IDialogManager>();

            Initialize();

            this.Loaded += OnLoaded;
            this.Unloaded += OnUnloaded;
            this.Closed += OnClosed;
        }

        protected virtual void OnClosed(object sender, EventArgs e)
        {
            if(_DialogManager is DialogManager InternalDialogMgr)
            {
                InternalDialogMgr.DialogChanged -= OnDialogChanged;
            }
        }

        protected virtual void OnUnloaded(object sender, RoutedEventArgs e)
        {
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
        }


        //-------------------------------------------------------------------------------------------------
        //
        //  Initialize Methods
        //
        //-------------------------------------------------------------------------------------------------
        void Initialize()
        {
            InitializeDialogCommands();

            //
            // 初始化
            InitializeDialogManager();
        }

        void InitializeDialogCommands()
        {
            //
            // 绑定命令
            CommandBindings.Add(new CommandBinding(WindowsCommands.Ok, null, null));
            CommandBindings.Add(new CommandBinding(WindowsCommands.Cancel, null, null));
            CommandBindings.Add(new CommandBinding(WindowsCommands.Next, null, null));
            CommandBindings.Add(new CommandBinding(WindowsCommands.Last, null, null));
        }

        void InitializeDialogManager()
        {
            if(_DialogManager is DialogManager InternalDialogMgr)
            {
                InternalDialogMgr.DialogChanged += OnDialogChanged;
            }
        }

        private void OnDialogChanged(IViewModel DialogVM)
        {
            //
            // 
            SetValue(DialogPropertyKey, DialogVM);
        }
    }
}
