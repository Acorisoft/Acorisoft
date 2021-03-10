using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Acorisoft.Morisa.Core
{
    public interface IModelObject : IUnique, INotifyPropertyChanging, INotifyPropertyChanged
    {
    }
}
