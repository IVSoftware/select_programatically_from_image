using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace select_programatically_from_image
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            BindingContext = new MainFrameBinding();
            InitializeComponent();
            _debug = Flist;
        }
        public static ListView _debug = null;
    }

    class MainFrameBinding : INotifyPropertyChanged
    {
        public MainFrameBinding()
        {
            DataModel.Delete += (sender, e) =>
            {
                Items.Remove((DataModel)sender);
            };
            DataModel.Select += (sender, e) =>
            {
                SelectedItem = (DataModel)sender;
            };
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
        DataModel _SelectedItem = null;
        public DataModel SelectedItem
        {
            get => _SelectedItem;
            set => SetProperty(ref _SelectedItem, value);
        }

    }
    class DataModel : INotifyPropertyChanged
    {
        public DataModel()
        {
            DeleteFileWithPromptCommand = new Command<DataModel>(OnDeleteFileWithPromptCommand);
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

        string _fname = string.Empty;
        public string fname
        {
            get => _fname;
            set => SetProperty(ref _fname, value);
        }

        public ICommand DeleteFileWithPromptCommand { get; private set; }
        private async void OnDeleteFileWithPromptCommand(DataModel item)
        {
            Select?.Invoke(this, EventArgs.Empty);
            var fileName = item.fname;
            if (await App.Current.MainPage.DisplayAlert(
                "delete file",
                fileName, accept: "OK",
                cancel: "CANCEL"))
            {
                Delete?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Cancelled!");
            }
        }
        public static event EventHandler Select;
        public static event EventHandler Delete;
    }
}
