using System;

namespace Acorisoft.Extensions.Platforms.Services
{
    public class ObservableOperation
    {
        private readonly Action _operation;
        public ObservableOperation(Action operation, string description)
        {
            Description = description;
            _operation = operation ?? throw new ArgumentNullException(nameof(operation));
        }

        public void Run() => _operation.Invoke();
        public string Description { get; }
    }
}