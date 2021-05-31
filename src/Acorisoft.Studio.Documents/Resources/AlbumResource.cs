﻿using System;
using System.IO;
using Acorisoft.Studio.ProjectSystems;
using LiteDB;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Acorisoft.Studio.ProjectSystems;

namespace Acorisoft.Studio.Documents.Resources
{
    public class AlbumResource : ImageResource, ICollection<Guid>
    {
        private readonly List<Guid> _guids;

        public AlbumResource()
        {
            _guids = new List<Guid>();
        }
        
        public override string GetResourceFileName(IComposeSet composeSet)
        {
            if (composeSet != null && _guids.Count > 0)
            {
                return GetResourceFileName(composeSet, _guids.FirstOrDefault());
            }
            
            throw new InvalidOperationException("无法打开创作集");
        }
        
        public string GetResourceFileName(IComposeSet composeSet , Guid id)
        {
            if (composeSet != null && _guids.Count > 0)
            {
                return Path.Combine(composeSet.GetComposeSetPath(ComposeSetKnownFolder.Image), id.ToString("N"));
            }
            
            throw new InvalidOperationException("无法打开创作集");
        }
        
        public string[] GetResourceFileNames(IComposeSet composeSet)
        {
            if (composeSet != null && _guids.Count > 0)
            {
                return _guids.Select(x => GetResourceFileName(composeSet, x)).ToArray();
            }
            
            throw new InvalidOperationException("无法打开创作集");
        }

        public IEnumerator<Guid> GetEnumerator() => _guids.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _guids.GetEnumerator();

        public void Add(Guid item) => _guids.Add(item);

        public void Clear() => _guids.Clear();

        public bool Contains(Guid item) => _guids.Contains(item);

        public void CopyTo(Guid[] array, int arrayIndex) => _guids.CopyTo(array, arrayIndex);

        public bool Remove(Guid item) =>  _guids.Remove(item);

        public int Count => _guids.Count;
        public bool IsReadOnly => false;
    }
}