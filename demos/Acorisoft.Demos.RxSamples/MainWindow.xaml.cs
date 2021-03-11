using LiteDB;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Acorisoft.Demos.RxSamples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();


            var db = new LiteDatabase(new ConnectionString
            {
                Filename = "Test.MORISA-SETTING",
                Collation = new Collation(1033, System.Globalization.CompareOptions.IgnoreCase),
            });
            var id = "{10F9CA2E-BCE3-4A48-93F6-A138D749A74C}";
            var names = db.GetCollectionNames();
            foreach(var name in names)
            {
                Debug.WriteLine(name);
            }
            var stream = db.FileStorage.OpenRead(id);
            var bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = stream;
            bi.EndInit();
            PART_Image.Source = bi;
        }
    }
}
