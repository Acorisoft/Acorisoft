using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    public class DataTrackingState : IDataTrackingState
    {
        private volatile bool _IsTracking;

        class TrackingStateBacker : IDisposable
        {
            private DataTrackingState _state;

            public TrackingStateBacker(DataTrackingState state)
            {
                _state = state;
                _state._IsTracking = false;
            }

            public void Dispose()
            {
                _state._IsTracking = true;
            }
        }

        public IDisposable BeforeTracking()
        {
            return new TrackingStateBacker(this);
        }

        public void SetTrackingState(bool state) => _IsTracking = state;

        public bool IsTracking => _IsTracking;
    }
}
