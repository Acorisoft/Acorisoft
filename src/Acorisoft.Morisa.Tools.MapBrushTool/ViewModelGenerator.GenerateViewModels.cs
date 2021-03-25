 
 

using DryIoc;
using ReactiveUI;
using Acorisoft.Morisa.Tools.Views;
using Acorisoft.Morisa.Tools.ViewModels;

namespace Acorisoft.Morisa.Tools
{
    partial class App
    {
	    public void RegisterViews(IContainer container)
	    {
		    container.Register<NewBrushDialogViewFunction>();
			container.Register<IViewFor<NewBrushDialogViewFunction>,NewBrushDialogView>();
			container.Register<NewBrushesDialogViewFunction>();
			container.Register<IViewFor<NewBrushesDialogViewFunction>,NewBrushesDialogView>();
			container.Register<NewBrushGroupDialogViewFunction>();
			container.Register<IViewFor<NewBrushGroupDialogViewFunction>,NewBrushGroupDialogView>();
			container.Register<OpenBrushSetDialogViewFunction>();
			container.Register<IViewFor<OpenBrushSetDialogViewFunction>,OpenBrushSetDialogView>();
			container.Register<SaveBrushSetStep2ViewFunction>();
			container.Register<IViewFor<SaveBrushSetStep2ViewFunction>,SaveBrushSetStep2View>();
			container.Register<SaveBrushSetStepViewFunction>();
			container.Register<IViewFor<SaveBrushSetStepViewFunction>,SaveBrushSetStepView>();
			container.Register<HomeViewModel>();
			container.Register<IViewFor<HomeViewModel>,HomeView>();
	    }
    }
}