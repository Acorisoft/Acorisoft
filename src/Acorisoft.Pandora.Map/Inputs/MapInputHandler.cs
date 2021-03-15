using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Input.StylusPlugIns;

namespace Acorisoft.Pandora.Inputs
{
    public class MapInputHandler : StylusPlugIn, IObservable<StylusPoint>
    {
        private readonly Subject<StylusPoint> _PointSubject;

        public MapInputHandler()
        {
            _PointSubject = new Subject<StylusPoint>();
        }

        protected override void OnStylusEnter(RawStylusInput rawStylusInput, bool confirmed)
        {
            //
            // 我们不需要使用 Enter
            base.OnStylusEnter(rawStylusInput, confirmed);
        }

        protected override void OnStylusDown(RawStylusInput rawStylusInput)
        {
            if (!IsEnable)
            {
                return;
            }
            foreach (var stylePoint in rawStylusInput.GetStylusPoints())
            {
                _PointSubject.OnNext(stylePoint);
            }

            base.OnStylusDown(rawStylusInput);
        }

        protected override void OnStylusMove(RawStylusInput rawStylusInput)
        {
            if (!IsEnable)
            {
                return;
            }
            foreach (var stylePoint in rawStylusInput.GetStylusPoints())
            {
                _PointSubject.OnNext(stylePoint);
            }
            base.OnStylusMove(rawStylusInput);
        }

        protected override void OnStylusUp(RawStylusInput rawStylusInput)
        {
            if (!IsEnable)
            {
                return;
            }
            foreach (var stylePoint in rawStylusInput.GetStylusPoints())
            {
                _PointSubject.OnNext(stylePoint);
            }

            _PointSubject.OnCompleted();
            base.OnStylusUp(rawStylusInput);
        }

        protected override void OnStylusLeave(RawStylusInput rawStylusInput, bool confirmed)
        {
            if (!IsEnable)
            {
                return;
            }
            //
            // 注意:
            // StylusUp可能不会触发！
            // 如果用户的输入设备在一直按下的情况下离开了视觉元素的边界则不会触发StylusUp而是出发StylusLeave
            // 并且StylusLeave的参数可能为负数值，所以不能用作OnNext的参数
            _PointSubject.OnCompleted();
            base.OnStylusLeave(rawStylusInput, confirmed);
        }

        public IDisposable Subscribe(IObserver<StylusPoint> observer)
        {
            return ((IObservable<StylusPoint>)_PointSubject).Subscribe(observer);
        }

        public bool IsEnable { get; set; }
    }
}
