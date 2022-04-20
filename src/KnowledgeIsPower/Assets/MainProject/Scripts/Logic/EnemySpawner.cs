using System;
using CodeBase.StaticData;
using MainProject.Scripts.Data;
using MainProject.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace MainProject.Scripts.Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private MonsterTypeId monsterTypeId;
        
        private string _id;
        public bool _slain;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            if (playerProgress.KillData.ClearedSpawners.Contains(_id))
            {
                _slain = true;
            }
            else
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            if (_slain)
                playerProgress.KillData.ClearedSpawners.Add(_id);
        }
    }
}