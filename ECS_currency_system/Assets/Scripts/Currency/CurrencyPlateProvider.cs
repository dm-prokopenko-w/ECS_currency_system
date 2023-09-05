using System;
using UnityEngine;
using UnityEngine.UI;
using Voody.UniLeo;

namespace Currency
{
    public class CurrencyPlateProvider : MonoProvider<CurrencyPlateComponent>
    {
        public void Init(string id, int countOnStart, Action<string, int> OnUpdateCount, int countAddOnClick,
            int countRemoveOnClick)
        {
            value.Id = id;
            
            value.Title.text = id;
            value.Counter.text = countOnStart.ToString();
            
            value.AddBtn.onClick.AddListener(() => OnUpdateCount?.Invoke(id, countAddOnClick));
            value.RemoveBtn.onClick.AddListener(() => OnUpdateCount?.Invoke(id, countRemoveOnClick));
            value.ClearBtn.onClick.AddListener(() => OnUpdateCount?.Invoke(id, 0));
        }
    }

    [Serializable]
    public struct CurrencyPlateComponent
    {
        public string Id;
        public Button AddBtn;
        public Button RemoveBtn;
        public Button ClearBtn;
        public Text Title;
        public Text Counter;
    }
}