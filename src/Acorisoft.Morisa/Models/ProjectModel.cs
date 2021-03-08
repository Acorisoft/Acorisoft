using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Models
{
    public class ProjectModel
    {

        [Reactive]public string Name { get; set; }
        [Reactive]public string Path { get; set; }
        [Reactive]public string FileName { get; set; }
    }
}
