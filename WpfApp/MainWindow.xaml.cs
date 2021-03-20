using SelfHost1.ServiceModel;
using Pages = SelfHost1.ServiceModel.Types.Page;
using ServiceStack;
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
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IServiceClient client;
        public MainWindow()
        {
            InitializeComponent();

            client = new CachedHttpClient(new JsonHttpClient("http://localhost:8088"));

        }
        private async void GetRequest()
        {
            var client = new CachedHttpClient(new JsonHttpClient("http://localhost:8088"));

            await client.SendAsync<CreatePageResponse>( new CreatePage
            {
                Url = "www.vk.com",
                Html = "Html maybe",
                Title = "Title maybe",
                Text = "Text maybe",
                Date = DateTime.Now
            });
        }

        private async void InitialieGrid()
        {

            var result = await client.SendAsync<GetPagesResponse>(new GetPage());
            GridBase.ItemsSource = result.Result;
        }
        private void BtnGet_Click(object sender, RoutedEventArgs e)
        {
            InitialieGrid();
        }
    }
}
