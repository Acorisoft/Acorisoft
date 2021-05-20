using Acorisoft.Extensions.Windows.Views;
using DryIoc;

namespace Acorisoft.Extensions.Windows.Platforms
{
    public class Bootstrapper : Startup
    {
        public Bootstrapper()
        {
            //
            // 
            Container = ServiceProviderExtension.Init();
            
            ServiceProviderExtension.EnableLogger();
        }

        protected internal IContainer Container { get; }
    }
}