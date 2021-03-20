using Acorisoft.Morisa.Map;
using Acorisoft.Morisa.Tools.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Tools.Behaviors
{
    public class MapGroupSelector : TreeViewSelectorBehavior<IMapGroupAdapter>
    {
        protected override void OnItemSelected(object sender, IMapGroupAdapter newValue)
        {
            FrameworkElementExtension.SetSelectedItem(AssociatedObject, newValue);
        }
    }
}
