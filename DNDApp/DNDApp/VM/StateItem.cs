using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using DNDApp.Data;
using System.Linq;
using System;

namespace DNDApp.VM
{
    class StateItem : BaseItem
    {
        static List<StateItem> AllItemsForSave = new List<StateItem>();
        /// <summary>
        /// Crutch
        /// </summary>
        public static List<StateItem> AllItems = new List<StateItem>()
        {
            new StateItem()
            {
                Title = "Стрельба прицельная",
                UpgradeСostDescription = "11123"
            },
            new StateItem()
            {
                Title = "Стрельба навскидку",
                UpgradeСostDescription = "11123"
            },
            new StateItem()
            {
                Title = "Знание зоны",
                UpgradeСostDescription = "11223"
            },
            new StateItem()
            {
                Title = "Выживание",
                UpgradeСostDescription = "½½112"
            },
            new StateItem()
            {
                Title = "Рукопашный бой",
                UpgradeСostDescription = "½½112"
            },
            new StateItem()
            {
                Title = "Лазанье+веревки",
                UpgradeСostDescription = "½½112"
            },
            new StateItem()
            {
                Title = "Маскировка",
                UpgradeСostDescription = "11123"
            },
            new StateItem()
            {
                Title = "Гранатомётчик",
                UpgradeСostDescription = "113__",
                Description = "Каждые 2lvl стрельбы дают +1"
            },
            new StateItem()
            {
                Title = "Водитель",
                UpgradeСostDescription = "1½½12"
            },
            new StateItem()
            {
                Title = "Водитель БТР",
                UpgradeСostDescription = "11___",
                Description = "3lvl и далее стакаются с водителем"
            },
            new StateItem()
            {
                Title = "Харизма",
                UpgradeСostDescription = "11112"
            },
            new StateItem()
            {
                Title = "Манипуляция",
                UpgradeСostDescription = "1½112"
            },
            new StateItem()
            {
                Title = "Анализ ответственности",
                UpgradeСostDescription = "11112"
            },
            new StateItem()
            {
                Title = "Анализ честности",
                UpgradeСostDescription = "11113"
            },
            new StateItem()
            {
                Title = "Внимательность",
                UpgradeСostDescription = "½½112"
            },
            new StateItem()
            {
                Title = "Медик-хирург",
                UpgradeСostDescription = "11223"
            },
            new StateItem()
            {
                Title = "Оружейный техник",
                UpgradeСostDescription = "11123"
            },
            new StateItem()
            {
                Title = "Автомеханик",
                UpgradeСostDescription = "1112",
                Description = "3lvl только при 1м сварщике"
            },
            new StateItem()
            {
                Title = "Сварщик",
                UpgradeСostDescription = "1½½½",
                Description = "два уровня +1 автоме, не может дать последний"
            },
            new StateItem()
            {
                Title = "Специалист по экзоскелетам",
                UpgradeСostDescription = "12333"
            },
            new StateItem()
            {
                Title = "Специалист по артефактам",
                UpgradeСostDescription = "__123",
                Description = "Первые 2 уровня это первые 2 уровня знания зоны"
            },
            new StateItem()
            {
                Title = "Сапёр",
                UpgradeСostDescription = "11123"
            },
            new StateItem()
            {
                Title = "Электрик",
                UpgradeСostDescription = "½½112"
            },
            new StateItem()
            {
                Title = "Столяр",
                UpgradeСostDescription = "½½111"
            },
            new StateItem()
            {
                Title = "Гитарист",
                UpgradeСostDescription = "1½½22"
            },
            new StateItem()
            {
                Title = "Повар",
                UpgradeСostDescription = "½½½22"
            },
            new StateItem()
            {
                Title = "Хакер",
                UpgradeСostDescription = "21212"
            },
            new StateItem()
            {
                Title = "Художник",
                UpgradeСostDescription = "11112"
            },
        };
        public event EventHandler UpdateEvent;
        public StateItem()
        {
            AllItemsForSave.Add(this);
            SelectCommand = new Command(OnSelect);
            SaveCommand = new Command(OnSave);
            AddPointCommand = new Command(OnAddPoint);
            RemovePointCommand = new Command(OnRemovePoint);
        }
        #region Propertyes
        public string UpgradeСostDescription { get; set; }
        public string Description { get; set; }
        float[] UpgradeСost
        {
            get
            {
                List<float> Result = new List<float>();
                foreach (var item in UpgradeСostDescription)
                {
                    if (int.TryParse(item.ToString(), out int value))
                        Result.Add(value);
                    else if (item == '½')
                        Result.Add(0.5F);
                    else
                        Result.Add(0);
                }
                return Result.ToArray();
            }
        }
        #endregion
        #region Commands
        void OnSelect(object obj)
        {
            IsEditable = true;
        }
        void OnSave(object obj)
        {
            IsEditable = false;
        }
        void OnAddPoint(object obj)
        {
            if (Amount < UpgradeСost.Length)
            {
                UpdateEvent?.Invoke(this, new NumericEventArgs() { Value = -UpgradeСost[Amount] });
                Amount++;
                DataKeeper.SaveStates(AllItemsForSave.Select(s => s.GetItemForSerialize()).ToList());
            }
        }
        public ICommand AddPointCommand { get; set; }
        void OnRemovePoint(object obj)
        {
            if (Amount > 0)
            {
                Amount--;
                UpdateEvent?.Invoke(this, new NumericEventArgs() { Value = UpgradeСost[Amount] });
            }
        }
        public ICommand RemovePointCommand { get; set; }
        #endregion
        public StateItemForSerialize GetItemForSerialize() => new StateItemForSerialize()
        {
            Title = Title,
            Amount = Amount
        };
    }
}