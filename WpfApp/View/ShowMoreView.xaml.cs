using SelfHost1.ServiceModel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp.ViewModel;

namespace WpfApp.View
{
    /// <summary>
    /// Логика взаимодействия для ShowMoreView.xaml
    /// </summary>
    public partial class ShowMoreView : Window
    {
        public ShowMoreView(Page pg)
        {
            InitializeComponent();
            this.DataContext = new ShowMoreViewModel(pg);
        }
    }
}
