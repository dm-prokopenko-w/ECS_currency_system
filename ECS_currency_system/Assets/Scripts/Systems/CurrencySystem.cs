using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using Save;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Currency
{
    public class CurrencySystem : SaveManager, IEcsInitSystem
    {
        private EcsWorld _world = null;
        private readonly AllConfigs _allConfig = default;

        private EcsFilter<CurrencyPlatesComponent> _parent = null;
        private EcsFilter<CurrencyPlateComponent> _plates = null;

        private EcsFilter<SaveCurrencyBtn> _saveBtn = null;
        private EcsFilter<LoadCurrencyBtn> _loadBtn = null;
        private EcsFilter<DeleteCurrencyBtn> _deleteBtn = null;

        private CurrencyConfig _config;
        private SaveDataCurrency _save = new SaveDataCurrency();
        private SaveDataCurrency _currentData = new SaveDataCurrency();

        public void Init()
        {
            _config = (CurrencyConfig)_allConfig.AllConfigList.Find(x => x.ID.Equals(Constants.CurrencyConfigId));
            if (_config == null)
            {
                Debug.LogError("Currency config not added.");
                return;
            }

            foreach (var date in _config.Datas)
            {
                if (!PlayerPrefs.HasKey(date.Id))
                {
                    PlayerPrefs.SetInt(date.Id, date.CountOnStart);
                }
            }

            InitCurrencyPlate();

            foreach (var i in _saveBtn)
            {
                _saveBtn.Get1(i).ButtonSave.onClick.AddListener(SaveOnClick);
            }

            foreach (var i in _loadBtn)
            {
                _loadBtn.Get1(i).ButtonLoad.onClick.AddListener(LoadOnClick);
            }

            foreach (var i in _deleteBtn)
            {
                _deleteBtn.Get1(i).DeleteSave.onClick.AddListener(DeleteOnClick);
            }
        }

        private void LoadOnClick()
        {
            string json = LoadSave(Constants.CurrencySave);
            _save = JsonUtility.FromJson<SaveDataCurrency>(json);

            if (_save == null) return;
            
            foreach (var save in _save.CurrencyData)
            {
                UpdateText(save.Currency, save.Count);
            }
        }

        private void DeleteOnClick()
        {
            Delete(Constants.CurrencySave);
        }

        private void SaveOnClick()
        {
            if (_save == null) _save = new SaveDataCurrency();
            if (_save.CurrencyData == null) _save.CurrencyData = new List<DataCurrency>();
            
            foreach (var data in _config.Datas)
            {
                var save = _save.CurrencyData.Find(x => x.Currency.Equals(data.Id));
                if (save == null)
                {
                    _save.CurrencyData.Add(new DataCurrency()
                    {
                        Currency = data.Id,
                        Count = GetCountCurrencyById(data.Id)
                    });
                }
                else
                {
                    var current = _save.CurrencyData.Find(x => x.Currency.Equals(data.Id));
                    save.Count = current.Count;
                }
            }

            _save.Id = Constants.CurrencySave;
            SaveAsync(Constants.CurrencySave, _save);
        }

        private void InitCurrencyPlate()
        {
            _currentData.CurrencyData = new List<DataCurrency>();
            _currentData.Id = Constants.CurrencySave;

            foreach (var i in _parent)
            {
                foreach (var data in _config.Datas)
                {
                    var plate = Object.Instantiate(_config.Prefab, _parent.Get1(i).ParrentPlates);

                    var current = _currentData.CurrencyData.Find(x => x.Currency.Equals(data.Id));
                    if (current == null)
                    {
                        _currentData.CurrencyData.Add(new DataCurrency()
                        {
                            Currency = data.Id,
                            Count = GetCountCurrencyById(data.Id)
                        });
                    }
                    else
                    {
                        current.Count = GetCountCurrencyById(data.Id);
                    }

                    plate.Init(
                        data.Id,
                        GetCountCurrencyById(data.Id),
                        UpdateText,
                        data.CountAddOnClick,
                        data.CountRemoveOnClick);
                }
            }
        }

        private void UpdateText(string id, int count)
        {
            foreach (var i in _plates)
            {
                if (_plates.Get1(i).Id.Equals(id))
                {
                    int countCurrency = PlayerPrefs.GetInt(id);

                    if (count == 0)
                    {
                        countCurrency = count;
                    }
                    else
                    {
                        countCurrency += count;
                    }

                    if (countCurrency < 0) return;
                    _plates.Get1(i).Counter.text = countCurrency.ToString();
                    PlayerPrefs.SetInt(id, countCurrency);
                }
            }
        }

        private int GetCountCurrencyById(string id)
        {
            var date = _config.Datas.Find(x => x.Id.Equals(id));
            if (date == null) return 0;

            return !PlayerPrefs.HasKey(date.Id) ? date.CountOnStart : PlayerPrefs.GetInt(date.Id);
        }
    }

    [Serializable]
    public class SaveDataCurrency
    {
        public string Id;
        public List<DataCurrency> CurrencyData;
    }

    [Serializable]
    public class DataCurrency
    {
        public string Currency;
        public int Count;
    }
}