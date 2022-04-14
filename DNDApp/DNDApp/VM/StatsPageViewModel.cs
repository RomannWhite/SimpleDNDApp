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
            AddPointCommand = new Command(OnAddPoint);
            RemovePointCommand = new Command(OnRemovePoint);
            States = new ObservableCollection<StateItem>(StateItem.AllItems);
            //States = new ObservableCollection<StateItem>()
            //{
            //    new StateItem()
            //    {
            //        Title = "Test 1",
            //        Amount = 0,
            //        Description = "Description",
            //        UpgradeСostDescription = "_½1222"
            //    },
            //    new StateItem()
            //    {
            //        Title = "Test 2",
            //        Amount = 0,
            //        Description = "Description",
            //        UpgradeСostDescription = "_½½11"
            //    },
            //    new StateItem()
            //    {
            //        Title = "Test 3",
            //        Amount = 0,
            //        Description = "Description",
            //        UpgradeСostDescription = "_½½12"
            //    }
            //};
            foreach (var item in DataKeeper.LoadStates())
            {
                StateItem CurrentState = States.FirstOrDefault(s => s.Title == item.Title);
                if (CurrentState != null)
                    CurrentState.Amount = item.Amount;
            }
            foreach (var item in States)
                item.UpdateEvent += Item_UpdateEvent;
        }
        void Item_UpdateEvent(object sender, System.EventArgs e)
        {
            if (e is NumericEventArgs NumericEventArg)
                FreePoints += NumericEventArg.Value;
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
        public float FreePoints
        {
            get => DataKeeper.LoadData(DataKeeper.FreePointsTag);
            set
            {
                DataKeeper.SaveData(value, DataKeeper.FreePointsTag);
                OnPropertyChanged();
            }
        }
        #endregion
        #region Commands
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
        #endregion
    }
}
