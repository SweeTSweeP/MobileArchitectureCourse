using System;
using MainProject.Scripts.Data;
using TMPro;
using UnityEngine;

namespace MainProject.Scripts.UI
{
    public class LootCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counter;
        private WorldData _worldData;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
            _worldData.LootData.Changed += UpdateCounter;
        }

        private void Start()
        {
            UpdateCounter();
        }

        private void UpdateCounter()
        {
            _counter.text = $"{_worldData.LootData.Collected}";
        }
    }
}