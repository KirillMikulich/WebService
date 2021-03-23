using SelfHost1.ServiceModel.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp.Model;

namespace WpfApp.ViewModel
{
    public class ShowMoreViewModel: INotifyPropertyChanged
    {
        private int Id { get; set; }
        private string title { get; set; }
        private string html { get; set; }
        private string text { get; set; }
        private string url { get; set; }
        private DateTime date { get; set; }

        public string Title
        {
            get { return title; }
            set
            {
                if (value != null)
                    title = value;
                OnPropertyChanged("Title");
            }
        }
        public string Html
        {
            get { return html; }
            set
            {
                if (value != null)
                    html = value;
                OnPropertyChanged("Html");
            }
        }
        public string Text
        {
            get { return text; }
            set
            {
                if (value != null)
                    text = value;
                OnPropertyChanged("Text");
            }
        }
        public string Url
        {
            get { return url; }
            set
            {
                if (value != null)
                    url = value;
                OnPropertyChanged("Url");
            }
        
        }
        public DateTime Date
        {
            get { return date; }
            set
            {
                if (value != null)
                    date = value;
                OnPropertyChanged("Date");
            }
        }

        public ICommand SaveCommand { get; set; }

        public ShowMoreViewModel(Page pg)
        {
            Id = pg.Id;
            Title = pg.Title;
            Html = pg.Html;
            Text = pg.Text;
            Url = pg.Url;
            Date = pg.Date;
            SaveCommand = new RelayCommand(o => ShowMoreModel.UdpatePageById(Id, Title, Text, Html, Date, Url));

        }
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
