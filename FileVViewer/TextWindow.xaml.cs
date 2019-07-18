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
using System.Windows.Shapes;

namespace FileVViewer
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class TxtWindow : Window
    {
        public TxtWindow()
        {
            InitializeComponent();
        }
        // Заполняем содержимым при открытии
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FileTextBlock.Text = HelperDir.ReadTxtFile(Title);
        }
    }
}
