using SelfHost1.ServiceModel.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp.Model;
using WpfApp.View;

namespace WpfApp.ViewModel
{
    public class MainWindowVewModel : INotifyPropertyChanged 
    {
        public ObservableCollection<Page> Pages { get; private set; }
        public ICommand ButtonLoadTable { get; set; }
        public ICommand UpdateArticles { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ShowMoreCommand { get; set; }
        public Page SelectedItem { get; set; }
       

        public MainWindowVewModel()
        {
            ButtonLoadTable = new RelayCommand(o => Task.Run(LoadPagesClick));
            UpdateArticles = new RelayCommand(o => UpdateArticlesClick());
            DeleteCommand = new RelayCommand(o => DeleteCommandClick());
            ShowMoreCommand = new RelayCommand(o => ShowMoreCommandClick());
        } 

        public void ShowMoreCommandClick()
        {
            if (SelectedItem != null)
            {
                new ShowMoreView(SelectedItem).ShowDialog();
                Pages = null; OnPropertyChanged("Pages");
                Task.Run(LoadPagesClick);
            }
        }

        public void DeleteCommandClick()
        {
            if (SelectedItem != null)
            {
                TableModel.DeleteArticlesAsyncById(SelectedItem.Id);
                Pages = null; OnPropertyChanged("Pages");
                Task.Run(LoadPagesClick);
            }
        }

        public  void UpdateArticlesClick()
        {
             TableModel.UpdateArticlesAsync();
        }
        
        public async Task LoadPagesClick()
        {
            Pages = new ObservableCollection<Page>(await TableModel.GettAllPagesAsync());
            //System.Windows.MessageBox.Show("Load");
            if (Pages != null)
                OnPropertyChanged("Pages");
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
