using System;
using CodeBase.StaticData;
using MainProject.Scripts.Data;
using MainProject.Scripts.Enemy;
using MainProject.Scripts.Infrastructure.Factory;
using MainProject.Scripts.Infrastructure.Services;
using MainProject.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace MainProject.Scripts.Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private MonsterTypeId monsterTypeId;
        [SerializeField] private bool slain;

        public bool Slain => slain;
        
        private string _id;
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
            _factory = AllServices.Container.Single<IGameFactory>();
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            if (playerProgress.KillData.ClearedSpawners.Contains(_id))
                slain = true;
            else
                Spawn();
        }

        private void Spawn()
        {
            var monster = _factory.CreateMonster(monsterTypeId, transform);
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.Happened += Slay;
        }

        private void Slay()
        {
            if (_enemyDeath != null)
                _enemyDeath.Happened -= Slay;
           
            slain = true;
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            if (slain)
                playerProgress.KillData.ClearedSpawners.Add(_id);
        }
    }
}