using Acorisoft.Spa;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Studio.ViewModels
{
    partial class ViewModelBase : IParameterViewModel
    {
        protected virtual void Parameter(IReadOnlyDictionary<string, object> parameters)
        {

        }

        public string UrlPathSegment { get; }

        public IScreen HostScreen { get => Screen; }

        void IParameterViewModel.Parameter(IReadOnlyDictionary<string, object> parameters)
        {
        }
    }
}
