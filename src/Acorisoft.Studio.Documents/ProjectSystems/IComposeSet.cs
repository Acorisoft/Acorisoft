using System;
using LiteDB;

namespace Acorisoft.Studio.ProjectSystems
{
    public interface IComposeSet : IDisposable
    {
        string GetComposeSetPath();

        string GetComposeSetPath(ComposeSetKnownFolder folder);
        IComposeSetProperty Property { get; set; }
    }

    interface IComposeSetDatabase
    {
        LiteDatabase MainDatabase { get; }
    }
}