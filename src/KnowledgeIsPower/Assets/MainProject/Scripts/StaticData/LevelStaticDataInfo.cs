using System.Collections.Generic;
using UnityEngine;

namespace MainProject.Scripts.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Ruslan/StaticData/Level")]
    public class LevelStaticDataInfo : ScriptableObject
    {
        public string LevelKey;

        public List<EnemySpawnerData> EnemySpawners;
    }
}