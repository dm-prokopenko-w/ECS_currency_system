using System;
using System.Collections.Generic;
using UnityEngine;

namespace Currency
{
    [CreateAssetMenu(fileName = "Configs", menuName = "Configs/CurrencyConfig")]
    public class CurrencyConfig : Configs
    {
        public CurrencyPlateProvider Prefab;
        public List<CurrencyData> Datas;
    }

    [Serializable]
    public class CurrencyData
    {
        public string Id;
        public int CountOnStart;
        public int CountAddOnClick = 1;
        public int CountRemoveOnClick = -1;
    }
}
