namespace Acorisoft.Extensions.Windows
{
    public delegate void NavigateToViewEventHandler(object sender, NavigateToViewEventArgs e);

    public delegate void IxContentChangedEventHandler(object sender, IxContentChangedEventArgs e);

    public delegate void DialogShowingEventHandler(object sender, DialogShowingEventArgs e);

    public delegate void PromptShowingEventHandler(object sender, PromptShowingEventArgs e);
    public delegate void WizardShowingEventHandler(object sender, WizardShowingEventArgs e);
    
}