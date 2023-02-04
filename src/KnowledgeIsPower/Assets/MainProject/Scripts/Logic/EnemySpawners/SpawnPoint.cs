using CodeBase.StaticData;
using MainProject.Scripts.Data;
using MainProject.Scripts.Enemy;
using MainProject.Scripts.Infrastructure.Factory;
using MainProject.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace MainProject.Scripts.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeId MonsterTypeId;

        private bool slain;
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;

        public string Id { get; set; }

        public void Construct(IGameFactory factory) =>
            _factory = factory;

        public void LoadProgress(PlayerProgress playerProgress)
        {
            if (playerProgress.KillData.ClearedSpawners.Contains(Id))
                slain = true;
            else
                Spawn();
        }

        private void Spawn()
        {
            var monster = _factory.CreateMonster(MonsterTypeId, transform);
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
                playerProgress.KillData.ClearedSpawners.Add(Id);
        }
    }
}