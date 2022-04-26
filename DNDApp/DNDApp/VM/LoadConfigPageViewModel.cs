using System.Windows.Input;
using Xamarin.Forms;
using DNDApp.Data;
using System.Threading.Tasks;
using System.Threading;

namespace DNDApp.VM
{
    class LoadConfigPageViewModel : BaseViewModel
    {
        public LoadConfigPageViewModel()
        {
            ApplyCommand = new Command(OnApply);
            CancelCommand = new Command(OnCancel);
        }
        #region Commands
        void OnApply(object obj) => Task.Run(async () =>
        {
            await DataKeeper.SaveNewStats(ConfigText);
            Thread.Sleep(1500);
            DependencyService.Get<IQuitHelper>().Quit();
        });
        public ICommand ApplyCommand { get; set; }
        void OnCancel(object obj)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
        public ICommand CancelCommand { get; set; }
        #endregion
        string configtext;
        public string ConfigText
        {
            get => configtext;
            set
            {
                configtext = value;
                OnPropertyChanged();
            }
        }
    }
}