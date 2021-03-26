 
 

using DryIoc;
using ReactiveUI;
using Acorisoft.Morisa.Views;
using Acorisoft.Morisa.ViewModels;
namespace Acorisoft.Morisa
{
    partial class App
    {
	    public void RegisterViews(IContainer container)
	    {
		    container.Register<EmotionViewModel>();
container.Register<IViewFor<EmotionViewModel>,EmotionView>();
container.Register<GenerateCompositionSetViewModel>();
container.Register<IViewFor<GenerateCompositionSetViewModel>,GenerateCompositionSetView>();
container.Register<HomeViewModel>();
container.Register<IViewFor<HomeViewModel>,HomeView>();
container.Register<SelectProjectDirectoryViewModel>();
container.Register<IViewFor<SelectProjectDirectoryViewModel>,SelectProjectDirectoryView>();
	    }
    }
}