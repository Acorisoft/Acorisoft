using LiteDB;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Acorisoft.Demos.RxSamples
{
    public class ClassA
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();


            var db = new LiteDatabase("FILENAME=Test.MORISA-SETTING;Journal=False");
            var col = db.GetCollection<ClassA>("Hello");
            col.Insert(new ClassA());
            var id = "10F9CA2E";
            var names = db.GetCollectionNames();
            Debug.WriteLine(db.FileStorage.Exists(id));
            Debug.WriteLine(db.CollectionExists("Hello"));
            db.FileStorage.Upload(id , @"D:\ico_512x512.ico");
            var stream = db.FileStorage.OpenRead(id);
            foreach (var name in names)
            {
                Debug.WriteLine(name);
            }
            var bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = stream;
            bi.EndInit();
            PART_Image.Source = bi;
        }
    }
}
