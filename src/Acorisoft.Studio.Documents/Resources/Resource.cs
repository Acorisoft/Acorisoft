using System;
using Acorisoft.Studio.ProjectSystems;
using LiteDB;

namespace Acorisoft.Studio.Documents.Resources
{
    public abstract class Resource
    {
        public abstract string GetResourceFileName(IComposeSet composeSet);
    }
}