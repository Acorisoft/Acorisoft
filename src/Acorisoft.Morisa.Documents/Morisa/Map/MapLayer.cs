using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    public abstract class MapLayer : IMapLayer
    {
        internal virtual void OnAttachMapDocument(MapDocument document)
        {

        }

        internal virtual void OnDetachMapDocument()
        {

        }

        public abstract bool HitTest(int x, int y);

        public abstract bool HitTest(double x, double y);
    }
}
