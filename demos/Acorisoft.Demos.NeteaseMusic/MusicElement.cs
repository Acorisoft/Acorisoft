using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Acorisoft.Demos.NeteaseMusic
{
    /// <summary>
    /// </summary>
    public class MusicElement : Control
    {
        private const string BrowserName = "PART_Browser";

        private enum MusicElementFlag
        {
            Initialize,
            Finished
        }

        // <iframe frameborder="no" border="0" marginwidth="0" marginheight="0" width=330 height=86 src="//music.163.com/outchain/player?type=2&id=447087&auto=1&height=66"></iframe>
        static MusicElement()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MusicElement), new FrameworkPropertyMetadata(typeof(MusicElement)));
        }

        private WebBrowser PART_Browser;
        private MusicElementFlag _flag;

        public override void OnApplyTemplate()
        {
            PART_Browser = (WebBrowser)GetTemplateChild(BrowserName);
            SetWebBrowserSilent(PART_Browser, true);
            OnSourceChanged(this, new DependencyPropertyChangedEventArgs(SourceProperty, string.Empty, Source));
            base.OnApplyTemplate();
        }

        private void SetWebBrowserSilent(WebBrowser webBrowser, bool silent)
        {
            FieldInfo fi = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fi != null)
            {
                object browser = fi.GetValue(webBrowser);
                if (browser != null)
                    browser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, browser, new object[] { silent });
            }
        }



        public string Source
        {
            get => (string)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            "Source",
            typeof(string),
            typeof(MusicElement), 
            new PropertyMetadata(string.Empty,OnSourceChanged));

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (MusicElement)d;
            var source = e.NewValue.ToString();

            if (!string.IsNullOrEmpty(source) && string.CompareOrdinal(source, e.OldValue?.ToString()) != 0)
            {
                if(element._flag == MusicElementFlag.Initialize)
                {
                    element._flag = MusicElementFlag.Finished;
                    return;
                }

                var newSource = $@"<iframe frameborder=""no"" border=""0"" marginwidth=""0"" marginheight=""0"" width=330 height=86 src=""https://music.163.com/outchain/player?type=2&id={source}&auto=0&height=66""></iframe>";
                var html = @$"<!DOCTYPE html><html lang=""en""><head><meta charset=""UTF-8""><meta http-equiv=""X-UA-Compatible"" content=""IE=edge""><meta name=""viewport"" content=""width=device-width,initial-scale=1.0""></head><body>{newSource}</body></html>";
                element.PART_Browser.NavigateToString(html);
            }
        }
    }
}
