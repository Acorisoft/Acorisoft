﻿using Acorisoft.Extensions.Platforms.Windows.Views;
using Acorisoft.Studio.ViewModels;
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
using ReactiveUI;
using ReactiveUI.Validation;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Extensions;

namespace Acorisoft.Studio.Views
{
    /// <summary>
    /// NewProjectDialog.xaml 的交互逻辑
    /// </summary>
    public partial class NewProjectDialog : DialogPage<NewProjectDialogViewModel>
    { 
        public NewProjectDialog()
        {
            InitializeComponent();

            this.BindValidation(ViewModel, x => x.Name, v => v.ProjectName.Text);
            
        }
    }
}
