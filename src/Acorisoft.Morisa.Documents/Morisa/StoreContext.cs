using Acorisoft.Morisa.Core;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public class StoreContext : Bindable, IStoreContext
    {
        private string _Name;
        [BsonId]
        public string Name { get => _Name; set => Set(ref _Name, value); }
        public string FileName { get; set; }
        public string Directory { get; set; }
    }
}
