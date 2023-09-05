using System;
using UnityEngine;
using Voody.UniLeo;

namespace Currency
{
    public class CurrencyPlatesProvider : MonoProvider<CurrencyPlatesComponent>
    {
    }
    
    [Serializable]
    public struct CurrencyPlatesComponent
    {
        public Transform ParrentPlates;
    }
}
