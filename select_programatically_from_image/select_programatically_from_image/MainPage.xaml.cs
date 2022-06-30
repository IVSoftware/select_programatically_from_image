using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace select_programatically_from_image
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            BindingContext = new MainFrameBinding();
            InitializeComponent();
        }

        private async void delete_Clicked(object sender, EventArgs e)
        {
            if (sender is ImageButton imageButton)
            {
                Flist.SelectedItem = imageButton.BindingContext;
                var fileName = ((DataModel)Flist.SelectedItem).fname;
                if (await App.Current.MainPage.DisplayAlert(
                    "delete file",
                    fileName, accept: "OK",
                    cancel: "CANCEL"))
                {
                    System.Diagnostics.Debug.WriteLine($"DELETED {fileName}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Cancelled!");
                }
            }
        }

        private async void Flist_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        }
    }

    class MainFrameBinding : INotifyPropertyChanged
    {
        public MainFrameBinding()
        {
            Items.Add(new DataModel { fname = "rtegdgg.txt" });
            Items.Add(new DataModel { fname = "mmrtegdgg.txt" });
        }
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetProperty<T>(
           ref T backingStore,
           T value,
           [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }
            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion INotifyPropertyChanged
        public ObservableCollection<DataModel> Items { get; set; } = new ObservableCollection<DataModel>();
    }
    class DataModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetProperty<T>(
           ref T backingStore,
           T value,
           [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }
            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion INotifyPropertyChanged

        string _fname = string.Empty;
        public string fname
        {
            get => _fname;
            set => SetProperty(ref _fname, value);
        }
    }
}
