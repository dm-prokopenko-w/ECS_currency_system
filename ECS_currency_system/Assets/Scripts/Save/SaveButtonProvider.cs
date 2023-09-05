using System;
using UnityEngine.UI;
using Voody.UniLeo;

namespace Save
{
    public class SaveButtonProvider : MonoProvider<SaveCurrencyBtn>
    {
    }

    [Serializable]
    public struct SaveCurrencyBtn
    {
        public Button ButtonSave;
    }
}
