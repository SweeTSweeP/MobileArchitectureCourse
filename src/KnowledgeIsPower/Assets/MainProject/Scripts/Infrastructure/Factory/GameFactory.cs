using System.Collections.Generic;
using CodeBase.StaticData;
using MainProject.Scripts.Enemy;
using MainProject.Scripts.Infrastructure.AssetManagement;
using MainProject.Scripts.Infrastructure.Services.PersistentProgress;
using MainProject.Scripts.Logic;
using MainProject.Scripts.StaticData;
using MainProject.Scripts.UI;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace MainProject.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        
        private GameObject HeroGameObject { get; set; }

        public GameFactory(IAssetProvider assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
        }

        public GameObject CreateHero(GameObject at)
        {
            HeroGameObject = InstantiateRegistered(AssetPath.HeroPath, at.transform.position);

            return HeroGameObject;
        }

        public GameObject CreateMonster(MonsterTypeId typeId, Transform parent)
        {
            var monsterData = _staticData.ForMonster(typeId);
            var monster = Object.Instantiate(monsterData.prefab, parent.position, Quaternion.identity, parent);

            var health = monster.GetComponent<IHealth>();
            health.Current = monsterData.Hp;
            health.Max = monsterData.Hp;

            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<AgentMoveToPlayer>().Construct(HeroGameObject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;
            var attack = monster.GetComponent<Attack>();
            attack.Construct(HeroGameObject.transform);
            attack.Damage = monsterData.Damage;
            attack.Cleavage = monsterData.Cleavage;
            attack.EffectiveDistance = monsterData.EffectiveDistance;
            
            monster.GetComponent<RotateToHero>()?.Construct(HeroGameObject.transform);
            
            return monster;
        }

        public GameObject CreateHud() => 
            InstantiateRegistered(AssetPath.UiPath);

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            var gameObject = _assets.Instantiate(prefabPath, at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            var gameObject = _assets.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);
            
            ProgressReaders.Add(progressReader);
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (var progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }
    }
}