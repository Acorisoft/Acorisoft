using System.Windows.Controls;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Extensions.Platforms.Windows.Views;

namespace Acorisoft.Extensions.Platforms.Windows.Views
{
    public partial class CloseWindowPromptDialog : DialogPage<CloseWindowPromptViewModel>
    {
        public CloseWindowPromptDialog()
        {
            InitializeComponent();
        }
    }
}