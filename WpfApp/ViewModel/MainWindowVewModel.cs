using SelfHost1.ServiceModel.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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

        private string _searchWord { get; set; }
        public string SearchWord { get {
                return _searchWord;
            } set {
                if(_searchWord != value)
                {
                    _searchWord = value;
                    OnPropertyChanged("SearchWord");
                }
            } }
        public ICommand SearchWordCommand { get; set; }

        private string _searchEntityes { get; set; }
        public string SearchEntityes
        {
            get
            {
                return _searchEntityes;
            }
            
            set
            {
                if (_searchEntityes != value)
                {
                    _searchEntityes = value;
                    OnPropertyChanged("SearchEntityes");
                }
            }
        }
        
        public ICommand SearchEntityesCommand { get; set; }

        public MainWindowVewModel()
        {
            
            ButtonLoadTable = new RelayCommand(o => {
                var preloader = new PreloaderView();
                preloader.Show();
                Task.Run(LoadPagesClick).Wait();
                preloader.Close();
            });
            UpdateArticles = new RelayCommand(o => {
                PreloaderView preloader = new PreloaderView();
                preloader.Show();
                UpdateArticlesClick();
                preloader.Close();
            });
            DeleteCommand = new RelayCommand(o => {
                DeleteCommandClick();
            });
            ShowMoreCommand = new RelayCommand(o =>
            {
                ShowMoreCommandClick();
            });
            SearchWordCommand = new RelayCommand(o => {
                PreloaderView preloader = new PreloaderView();
                preloader.Show();
                Task.Run(SearchWordClick).Wait();
                preloader.Close();
            });
            SearchEntityesCommand = new RelayCommand(o =>
            {
                PreloaderView preloader = new PreloaderView();
                preloader.Show();
                Task.Run(SearchEntityClick).Wait();
                preloader.Close();
            });
        } 

        public async Task SearchWordClick()
        {
            if(SearchWord.Length > 0)
            {
                
                Pages = new ObservableCollection<Page>(await TableModel.SearchWordAsync(SearchWord));
                OnPropertyChanged("Pages");
            }
        }

        public async Task SearchEntityClick()
        {
            if (SearchEntityes.Length > 0)
            {
                
                Pages = new ObservableCollection<Page>(await TableModel.SearchEntityAsync(SearchEntityes));
                OnPropertyChanged("Pages");
            }
        }

        public void ShowMoreCommandClick()
        {
            if (SelectedItem != null)
            {
                new ShowMoreView(SelectedItem).ShowDialog();
                
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
