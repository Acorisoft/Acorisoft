using LiteDB;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public class MorisaProjectInfo : ReactiveObject, IMorisaProjectInfo
    {
        [BsonId]
        [Reactive]public string Name { get; set; }
        [Reactive]public string FileName { get; set; }
        [Reactive]public string Directory { get; set; }
    }
}
