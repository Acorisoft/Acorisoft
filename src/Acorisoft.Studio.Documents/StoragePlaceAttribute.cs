﻿using System;

namespace Acorisoft.Studio
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public class StoragePlaceAttribute : Attribute
    {
        public StoragePlaceAttribute(StorageClassifier place)
        {
        }
        
        public StoragePlaceAttribute(StorageClassifier place, string description)
        {
        }
    }
}