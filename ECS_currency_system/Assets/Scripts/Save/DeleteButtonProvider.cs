using System;
using UnityEngine.UI;
using Voody.UniLeo;

namespace Save
{
    public class DeleteButtonProvider : MonoProvider<DeleteCurrencyBtn>
    {
    }

    [Serializable]
    public struct DeleteCurrencyBtn
    {
        public Button DeleteSave;
    }
}
