using System;

namespace Acorisoft.Extensions.Windows.Threadings
{
    public class ObservableOperation
    {
        public ObservableOperation(Action operation, string key)
        {
            Description = key;
            Operation = operation ?? throw new ArgumentNullException(nameof(operation));
        }
        
        
        public Action Operation { get; }

        public string Description { get; }
    }
}