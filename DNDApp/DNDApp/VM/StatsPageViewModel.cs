using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using DNDApp.Data;
using System.Linq;

namespace DNDApp.VM
{
    class StatsPageViewModel : BaseViewModel
    {
        public StatsPageViewModel()
        {
            LoadConfigCommand = new Command(OnLoadConfig);
            AddPointCommand = new Command(OnAddPoint);
            RemovePointCommand = new Command(OnRemovePoint);
            EditMainCommand = new Command(OnEditMain);
            SaveMainCommand = new Command(OnSaveMain);
            States = new ObservableCollection<StateItem>(DataKeeper.LoadStates());
            foreach (var item in States)
                item.UpdateEvent += Item_UpdateEvent;
        }
        void Item_UpdateEvent(object sender, System.EventArgs e)
        {
            if (e is NumericEventArgs NumericEventArg)
                FreePoints += NumericEventArg.Value;
            DataKeeper.SaveStates(States.ToList());
        }
        #region Propertyes
        ObservableCollection<StateItem> states;
        public ObservableCollection<StateItem> States
        {
            get => states;
            set
            {
                states = value;
                OnPropertyChanged();
            }
        }
        public int FreePoints
        {
            get => DataKeeper.LoadData(DataKeeper.FreePointsTag);
            set
            {
                DataKeeper.SaveData(value, DataKeeper.FreePointsTag);
                OnPropertyChanged();
            }
        }
        public int Force
        {
            get => DataKeeper.LoadData(DataKeeper.ForceTag);
            set
            {
                DataKeeper.SaveData(value, DataKeeper.ForceTag);
                OnPropertyChanged();
            }
        }
        public int Agility
        {
            get => DataKeeper.LoadData(DataKeeper.AgilityTag);
            set
            {
                DataKeeper.SaveData(value, DataKeeper.AgilityTag);
                OnPropertyChanged();
            }
        }
        public int Endurance
        {
            get => DataKeeper.LoadData(DataKeeper.EnduranceTag);
            set
            {
                DataKeeper.SaveData(value, DataKeeper.EnduranceTag);
                OnPropertyChanged();
            }
        }
        bool ismaineditable;
        public bool IsMainEditable
        {
            get => ismaineditable;
            set
            {
                ismaineditable = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Commands
        void OnLoadConfig(object obj)
        {
            Application.Current.MainPage.Navigation.PushAsync(new LoadConfigPage());
        }
        public ICommand LoadConfigCommand { get; set; }
        void OnAddPoint(object obj)
        {
            FreePoints++;
        }
        public ICommand AddPointCommand { get; set; }
        void OnRemovePoint(object obj)
        {
            if (FreePoints > 0)
                FreePoints--;
        }
        public ICommand RemovePointCommand { get; set; }
        public ICommand EditMainCommand { get; set; }
        void OnEditMain(object obj)
        {
            IsMainEditable = true;
        }
        public ICommand SaveMainCommand { get; set; }
        void OnSaveMain(object obj)
        {
            IsMainEditable = false;
        }
        #endregion
    }
}