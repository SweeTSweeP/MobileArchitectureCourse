using CodeBase.StaticData;
using UnityEngine;

namespace MainProject.Scripts.StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "Ruslan/StaticData/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeId MonsterTypeId;
        
        [Range(1, 100)]
        public int Hp;
        
        [Range(1, 30)]
        public float Damage;

        [Range(0.5f, 1)]
        public float EffectiveDistance = 0.666f;
        
        [Range(0.5f, 1)]
        public float Cleavage;
        
        [Range(0,10)]
        public float MoveSpeed = 3;
        
        public GameObject prefab;
    }
}