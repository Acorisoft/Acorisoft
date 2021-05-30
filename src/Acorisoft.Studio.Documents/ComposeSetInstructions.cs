using System;
using Acorisoft.Studio.ProjectSystems;
using MediatR;

namespace Acorisoft.Studio
{
    public class ComposeSetOpenInstruction : INotification
    {

        public ComposeSetOpenInstruction(IComposeSet composeSet)
        {
            ComposeSet = composeSet ?? throw new ArgumentNullException(nameof(composeSet));
        }
        public IComposeSet ComposeSet { get; }
        internal IComposeSetDatabase ComposeSetDatabase => (IComposeSetDatabase) ComposeSet;
    }
    
    public class ComposeSetSaveInstruction: INotification
    {
        
    }
    
    public class ComposeSetCloseInstruction: INotification
    {
    }
}