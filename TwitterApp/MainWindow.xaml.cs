using System;
using System.Linq;
using System.Windows;
using TwitterApp.Helpers;
using TwitterApp.ViewModels;

namespace TwitterApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetServiceData();
        }

        private void GetServiceData()
        {
            string error = null;
            try
            {
                var vm = new MainViewModel(true);

                var trendsCollection = TwitterHelper.GetTrends().Take<TwitterTrendViewModel>(8);
                foreach (var t in trendsCollection)
                {
                    vm.Trends.Add(new TwitterTrendViewModel { Name = t.Name });
                }

                var tweetsCollection = TwitterHelper.GetTweets().Take<TwitterPostViewModel>(5);
                foreach (var tw in tweetsCollection)
                {
                    vm.Posts.Add(new TwitterPostViewModel
                    {
                        AuthorImage = tw.User.ProfileImageUrl,
                        AuthorName = tw.User.UserName,
                        AuthorNick = tw.User.UserScreenName,
                        CreatedDate = tw.CreatedDate,
                        Text = tw.Text
                    });
                }

                var userInfo = TwitterHelper.GetUserInfo();
                if (userInfo != null)
                    vm.User = userInfo;

                this.DataContext = vm;
                return;
            }
            catch (TwitterException ex)
            {
                error = string.Format("Произошло специфическое исключение {0}:\n\t{1}", ex.GetType().Name, ex.Message);
            }
            catch (Exception ex)
            {
                error = string.Format("Произошло исключение {0}:\n\t{1}", ex.GetType().Name, ex.Message);
            }
            if (error != null)
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Close();
        }
        
    }
}
