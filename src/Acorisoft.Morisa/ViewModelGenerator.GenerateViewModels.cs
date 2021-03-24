


using Acorisoft.Morisa.ViewModels;
using Acorisoft.Morisa.Views;
using DryIoc;
using ReactiveUI;

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