using System;

namespace Acorisoft.Extensions.Platforms.Windows.ViewModels
{
    public interface IToastViewModel : IViewModel
    {
        public DateTime LastAccessBy { get; set; }
        public TimeSpan Offset { get; }
        public string Title { get; set; }
        public object Icon { get; set; }
    }

    public class ToastViewModel : IToastViewModel
    {
        public ToastViewModel(TimeSpan duration)
        {
            Offset = duration;
        }
        public DateTime LastAccessBy { get; set; }
        public TimeSpan Offset { get; }
    
        public string Title{ get; set; }

        public object Icon{ get; set; }
    }
}