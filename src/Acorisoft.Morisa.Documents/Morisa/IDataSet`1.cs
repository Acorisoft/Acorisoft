﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public interface IDataSet<TProperty> : IDataSet
        where TProperty : DataSetProperty
    {
    }
}