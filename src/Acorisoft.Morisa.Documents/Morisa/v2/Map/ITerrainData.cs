﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.v2.Map
{
    public interface ITerrainData : IMapData
    {
        Brush Brush { get; }
        int UnitX { get; }
        int UnitY { get; }
    }
}