using System.Collections.Generic;
using Xamarin.Essentials;
using Newtonsoft.Json;
using DNDApp.VM;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DNDApp.Data
{
    public class DataKeeper
    {
        public const string FreePointsTag = "FreePoints";
        public const string TotalMoneyTag = "TotalMoney";
        public const string ForceTag = "Force";
        public const string AgilityTag = "Agility";
        public const string EnduranceTag = "Endurance";
        const string AllInventoryItemTag = "AllInventoryItems";
        const string StateItemsTag = "StateItems";
        const string StateItemsValuesTag = "StateItemsValues";
        public static async Task SaveNewStats(string newstats)
        {
            List<string> ResultStats = new List<string>();
            if(!string.IsNullOrEmpty(newstats))
            {
                string[] NewStats = newstats.Split(new string[] { "/>" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string NewState in NewStats)
                {
                    string[] NewStateArray = NewState.Split(new char[] { '\t', '\n', '/', '<' }, StringSplitOptions.RemoveEmptyEntries);
                    if (NewStateArray.Length > 2)
                        ResultStats.Add($"{NewStateArray[0]}\t{NewStateArray[1]}\t{NewStateArray[2]}");
                    else if (NewStateArray.Length > 1)
                        ResultStats.Add($"{NewStateArray[0]}\t{NewStateArray[1]}");
                }
                await SecureStorage.SetAsync(StateItemsTag, string.Join("\n", ResultStats));
            }
        }
        public static void SaveStates(List<StateItem> value)
        {
            SecureStorage.SetAsync(StateItemsTag, string.Join("\n", value.Select(r => $"{r.Title}\t{r.UpgradeСostDescription}\t{r.Description}")));
            SecureStorage.SetAsync(StateItemsValuesTag, string.Join("\n", value.Select(r => $"{r.Title}\t{r.Amount}")));
        }
        public static List<StateItem> LoadStates()
        {
            string RawData = SecureStorage.GetAsync(StateItemsTag).Result;
            List<StateItem> Result = new List<StateItem>();
            if(RawData != null)
            {
                foreach (string RawDataItem in RawData.Split('\n'))
                {
                    string[] RawDataItemArray = RawDataItem.Split('\t');
                    if (RawDataItemArray.Length > 1)
                        Result.Add(new StateItem()
                        {
                            Title = RawDataItemArray[0],
                            UpgradeСostDescription = RawDataItemArray[1],
                        });
                    if (RawDataItemArray.Length > 2)
                        Result.Last().Description = RawDataItemArray[2];
                }

                string RawValuesData = SecureStorage.GetAsync(StateItemsValuesTag).Result;
                if(RawValuesData != null)
                    foreach (string RawValue in RawValuesData.Split('\n'))
                    {
                        if (RawValue.Split('\t').Length == 2)
                            if (int.TryParse(RawValue.Split('\t')[1], out int Value))
                                if (Result.FirstOrDefault(r => r.Title == RawValue.Split('\t')[0]) != null)
                                    Result.FirstOrDefault(r => r.Title == RawValue.Split('\t')[0]).Amount = Value;
                    }

                SecureStorage.SetAsync(StateItemsValuesTag, string.Join("\n", Result.Select(r => $"{r.Title}\t{r.Amount}")));
            }
            return Result;
        }
        public static void SaveInventory(List<InventoryItem> items)
        {
            string SerializedItems = JsonConvert.SerializeObject(items);
            SecureStorage.SetAsync(AllInventoryItemTag, SerializedItems);
        }
        public static void SaveData(int data, string key)
        {
            SecureStorage.SetAsync(key, data.ToString());
        }
        public static int LoadData(string key)
        {
            string stringdata = SecureStorage.GetAsync(key).Result;
            if (!string.IsNullOrEmpty(stringdata))
                if (float.TryParse(stringdata, out float result))
                    return (int)result;
            return 0;
        }
        public static List<InventoryItem> LoadInventory()
        {
            string SerializedItems = SecureStorage.GetAsync(AllInventoryItemTag).Result;
            if (!string.IsNullOrEmpty(SerializedItems))
                try
                {
                    return JsonConvert.DeserializeObject<List<InventoryItem>>(SerializedItems);
                }
                catch (System.Exception Ex)
                {
                    return new List<InventoryItem>()
                {
                    new InventoryItem()
                    {
                        Title = "Deserialize error"
                    }
                };
                }
            return new List<InventoryItem>();
        }
    }
}