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

namespace WpfApp.ViewModel
{
    public class MainWindowVewModel : INotifyPropertyChanged 
    {
        public ObservableCollection<Page> Pages { get; private set; }
        public ICommand ButtonCommand { get; set; }

        public MainWindowVewModel()
        {
            ButtonCommand = new RelayCommand(o => Task.Run(LoadPagesClick));
        } 
        
        public async Task LoadPagesClick()
        {
            Pages = new ObservableCollection<Page>(await TableModel.GettAllPagesAsync());
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
