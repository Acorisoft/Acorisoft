using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    public interface IDataTrackingState
    {
        IDisposable BeforeTracking();
        void SetTrackingState(bool state);
        bool IsTracking { get; }
    }
}
