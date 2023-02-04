using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData;
using UnityEngine;

namespace MainProject.Scripts.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticDataInfo> _levels;

        public void LoadMonsters()
        {
            _monsters = Resources.LoadAll<MonsterStaticData>("Enemy/StaticData")
                .ToDictionary(x => x.MonsterTypeId, x => x);
            _levels = Resources.LoadAll<LevelStaticDataInfo>("Level/StaticData")
                .ToDictionary(x => x.LevelKey, x => x);
        }

        public MonsterStaticData ForMonster(MonsterTypeId typeId) =>
            _monsters.TryGetValue(typeId, out MonsterStaticData staticData)
                ? staticData
                : null;

        public LevelStaticDataInfo FolLevel(string sceneName) =>
            _levels.TryGetValue(sceneName, out LevelStaticDataInfo staticData)
                ? staticData
                : null;
    }
}