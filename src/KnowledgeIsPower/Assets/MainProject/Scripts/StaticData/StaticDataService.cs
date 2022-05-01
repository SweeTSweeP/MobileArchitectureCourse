﻿using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData;
using UnityEngine;

namespace MainProject.Scripts.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;

        public void LoadMonsters()
        {
            _monsters = Resources.LoadAll<MonsterStaticData>("Enemy/StaticData")
                .ToDictionary(x => x.MonsterTypeId, x => x);
        }

        public MonsterStaticData ForMonster(MonsterTypeId typeId) => 
            _monsters.TryGetValue(typeId, out MonsterStaticData staticData) 
                ? staticData 
                : null;
    }
}