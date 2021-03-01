﻿using Acorisoft.Morisa.Dialogs;
using Acorisoft.Morisa.Routers;
using Acorisoft.Morisa.Samples;
using ReactiveUI;
using Splat;
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

namespace Acorisoft.Morisa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PART_Dialog.Manager = (DialogManager)Locator.Current.GetService<IDialogService>();
            var vm = Locator.Current.GetService<DialogSampleViewModel>();
            var screen = Locator.Current.GetService<IScreen>().Router.Navigate.Execute(vm);
        }
    }
}
