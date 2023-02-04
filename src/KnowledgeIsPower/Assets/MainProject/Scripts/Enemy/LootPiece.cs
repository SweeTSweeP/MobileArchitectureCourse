using System;
using System.Collections;
using MainProject.Scripts.Data;
using TMPro;
using UnityEngine;

namespace MainProject.Scripts.Enemy
{
    public class LootPiece : MonoBehaviour
    {
        [SerializeField] private GameObject _skull;
        [SerializeField] private GameObject _pickUpFxPrefab;
        [SerializeField] private TextMeshPro _lootText;
        [SerializeField] private GameObject _pickUpPopUp;
        
        private Loot _loot;
        private bool _picked;
        private WorldData _worldData;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
        }

        public void Initialize(Loot loot)
        {
            _loot = loot;
        }

        private void OnTriggerEnter(Collider other) => 
            PickUp();

        private void PickUp()
        {
            if (_picked) return;
            
            _picked = true;

            UpdateWorldData();
            HideSkull();
            PlayPickUpFx();
            ShowText();

            StartCoroutine(StartDestroyTimer());
        }

        private void UpdateWorldData()
        {
            _worldData.LootData.Collect(_loot);
        }

        private void HideSkull()
        {
            _skull.SetActive(false);
        }

        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(1.5f);
            
            Destroy(gameObject);
        }

        private void PlayPickUpFx()
        {
            Instantiate(_pickUpFxPrefab, transform.position, Quaternion.identity);
        }

        private void ShowText()
        {
            _lootText.text = $"{_loot.Value}";
            _pickUpPopUp.SetActive(true);
        }
    }
}