using Acorisoft.Extensions.Windows.Platforms;
using DryIoc;

namespace Acorisoft.Extensions.Windows
{
    public class Bootstrapper
    {
        private readonly IContainer _container;
        
        public Bootstrapper()
        {
            //
            // 
            _container = ServiceProviderExtension.Init();
        }
    }
}