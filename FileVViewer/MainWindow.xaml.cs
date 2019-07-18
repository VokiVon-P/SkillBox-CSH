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

        /// <summary>
        /// Открывает отдельное окно с содержимым текстового файла
        /// </summary>
        /// <param name="fileName"> название текстового файла </param>
        private void OpenFileInfo(string fileName)
        {
            if (HelperDir.IsTxtFile(fileName))
            {
                new TxtWindow()
                {
                    Owner = this,
                    Title = fileName
                }.Show();
            }
        }

        /// <summary>
        /// Реакция на активность по элементу из списка файлов
        /// </summary>
        private void SelectedAction()
        {
            string selectedItem = DirFileList.SelectedItem?.ToString();
            if (HelperDir.IsFile(selectedItem))
                OpenFileInfo(selectedItem);
            else
                GetDir(selectedItem);
        }

        
        // далее блок реакций на действия пользователя

        private void DirFileList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
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
