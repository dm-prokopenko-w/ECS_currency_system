using System;
using System.Collections.Generic;

namespace Save
{
    [Serializable]
    public class SaveDataCurrency : SaveData
    {
        public Dictionary<string, int> DataSave;
    }
}