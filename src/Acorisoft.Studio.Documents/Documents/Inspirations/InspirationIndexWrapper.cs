using System;
using System.Collections.Generic;

namespace Acorisoft.Studio.Documents.Inspirations
{
    public class InspirationIndexWrapper : DocumentIndexWrapper<InspirationIndex>, IComparable<InspirationIndexWrapper>
    {
        public InspirationIndexWrapper(InspirationIndex index) : base(index)
        {
        }

        public int CompareTo(InspirationIndexWrapper y)
        {
            return CompareTo((DocumentIndexWrapper<InspirationIndex>)y);
        }
        
        public string Summary
        {
            get => Source.Summary;
            set
            {
                Source.Summary = value;
                RaiseUpdated();
            }
        }
    }
}