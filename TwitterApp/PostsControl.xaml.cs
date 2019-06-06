using System.Windows;
using System.Windows.Controls;
using System.IO;
using System;
using System.Net;
using TwitterApp.ViewModels;
using TwitterApp.Helpers;


namespace TwitterApp
{
    /// <summary>
    /// Interaction logic for PostsControl.xaml
    /// </summary>
    public partial class PostsControl : UserControl
    {
        public PostsControl()
        {
            InitializeComponent();
        }

        private static void updatePostText(TextBlock tb, string text)
        {
            if (string.IsNullOrEmpty(text))
                tb.Text = null;
            else
            {
                Style linkStyle = tb.TryFindResource("postLink") as Style;

                tb.Inlines.Add(text);
                //tb.Inlines.Add(" -- ");
                //tb.Inlines.Add(new Run()
                //{
                //    Text = "#hash", Style = linkStyle

                //});
            }
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            TwitterPostViewModel post = tb.DataContext as TwitterPostViewModel;

            updatePostText(tb, post.Text);
        }

        private void SaveIconBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var twitsList = TwitterHelper.GetTweets();
                foreach (var itemInfo in twitsList)
                {
                    string fileName = itemInfo.User.UserName; ;
                    string imgUri = itemInfo.User.ProfileImageUrl;
                    string fileExt = Path.GetExtension(imgUri);
                    using (var webClient = new WebClient())
                    {
                        webClient.DownloadFileAsync(new Uri(imgUri), fileName + fileExt);
                    }
                }

                MessageBox.Show("Иконки авторов успешно записаны!");
            }
            catch (Exception ex)
            {
                throw new TwitterException("Не удалось записать иконки авторов", ex);
            }
        }
    }
}
