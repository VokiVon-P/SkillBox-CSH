using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using System.Collections.ObjectModel;

namespace FileVViewer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetDir();
        }

        private void GetDir(string dir = null)
        {
            // string temp = System.IO.Path.GetFullPath(".");
            if(dir == null)
                dir = Directory.GetCurrentDirectory();
            RightTextViewer.Text = dir;
            var listfile = new ObservableCollection<string>();
            foreach(var textDir in Directory.GetDirectories(dir))
            {
                listfile.Add(Path.GetFileName(textDir));
            }
            foreach (var textFile in Directory.GetFiles(dir))
            {
                listfile.Add(Path.GetFileName(textFile));
            }

            DirFileList.ItemsSource = listfile;

        }

        private void DirFileList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StringBuilder fileInfo = new StringBuilder();
            
            RightTextViewer.Text = DirFileList.SelectedItem.ToString();
        }
    }
}
