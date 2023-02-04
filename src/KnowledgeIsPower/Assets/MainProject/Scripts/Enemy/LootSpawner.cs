using MainProject.Scripts.Data;
using MainProject.Scripts.Infrastructure.Factory;
using MainProject.Scripts.Infrastructure.Services.Randomizer;
using UnityEngine;

namespace MainProject.Scripts.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        private IGameFactory _factory;
        private IRandomService _random;
        private int _lootMin;
        private int _lootMax;

        public void Construct(IGameFactory factory, IRandomService random)
        {
            _factory = factory;
            _random = random;
        }

        private void Start()
        {
            _enemyDeath.Happened += SpawnLoot;
        }

        private void SpawnLoot()
        {
            LootPiece loot = _factory.CreateLoot();
            loot.transform.position = transform.position;

            var lootItem = GenerateLoot();
            
            loot.Initialize(lootItem);
        }

        private Loot GenerateLoot() =>
            new Loot
            {
                Value = _random.Next(_lootMin, _lootMax)
            };

        public void SetLoot(int minLoot, int maxLoot)
        {
            _lootMin = minLoot;
            _lootMax = maxLoot;
        }
    }
}