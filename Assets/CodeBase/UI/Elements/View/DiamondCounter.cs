using System;
using CodeBase.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements.View
{
    public class DiamondCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counter;

        private WorldData _worldData;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
            _worldData.DiamondData.ValueChanged += UpdateCounter;
        }

        private void Start() => 
            UpdateCounter();

        private void UpdateCounter()
        {
            _counter.text = $"{_worldData.DiamondData.Value}";
        }
    }
}