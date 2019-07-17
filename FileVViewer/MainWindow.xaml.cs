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

        /// <summary>
        /// Заполняет левый лист бокс файлами и директориями
        /// </summary>
        /// <param name="dir"> директория для заполнения, если null - берем текущую </param>
        private void GetDir(string dir = null)
        {
            DirFileList.ItemsSource = HelperDir.GetFileDirList(dir);
        }

        private void GetFileInfo(string fileName)
        {
            RightTextViewer.Text = HelperDir.GetFileInfo(fileName);
        }

        private void SelectedAction()
        {
            string selectedItem = DirFileList.SelectedItem?.ToString();
            if (HelperDir.IsFile(selectedItem))
                GetFileInfo(selectedItem);
            else
                GetDir(selectedItem);
        }




        private void DirFileList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // StringBuilder fileInfo = new StringBuilder();
            
            RightTextViewer.Text = HelperDir.GetFileInfo(DirFileList.SelectedItem?.ToString());
        }

        private void DirFileList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectedAction();
        }

        private void DirFileList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
              SelectedAction();
        }
    }
}
