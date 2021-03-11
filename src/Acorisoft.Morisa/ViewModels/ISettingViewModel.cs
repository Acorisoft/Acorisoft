using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.ViewModels
{
    public interface ISettingViewModel
    {
        public bool IgnoreFileDuplicate
        {
            get; set;
        }
    }
}
