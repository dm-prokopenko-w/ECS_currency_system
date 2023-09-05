using System;
using UnityEngine.UI;
using Voody.UniLeo;

namespace Save
{
    public class LoadButtonProvider : MonoProvider<LoadCurrencyBtn>
    {
    }

    [Serializable]
    public struct LoadCurrencyBtn
    {
        public Button ButtonLoad;
    }
}