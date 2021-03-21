using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    public class MapDocument
    {
        private ITerrainLayer _Terrain;

        protected void OnLayerChanged(IMapLayer oldLayer, IMapLayer newLayer)
        {
            if (oldLayer is MapLayer oldLayerImpl)
            {
                oldLayerImpl.OnDetachMapDocument();
            }

            if (newLayer is MapLayer newLayerImpl)
            {
                newLayerImpl.OnAttachMapDocument(this);
            }
        }

        public ITerrainLayer Terrain
        {
            get => _Terrain;
            set
            {
                OnLayerChanged(_Terrain, value);
                _Terrain = value;
            }
        }
    }
}
