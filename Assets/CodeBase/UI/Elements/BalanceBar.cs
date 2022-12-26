using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class BalanceBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counter;
        public void SetValue(int diamondValue) => 
            _counter.text = diamondValue.ToString();
    }
}