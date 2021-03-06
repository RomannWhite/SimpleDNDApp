using Newtonsoft.Json;

namespace DNDApp.VM
{
    public class WeaponItem : SimpeItem
    {
        [JsonIgnore]
        int vl;
        public int Value
        {
            get => vl;
            set
            {
                vl = value;
                OnPropertyChanged();
            }
        }
    }
}