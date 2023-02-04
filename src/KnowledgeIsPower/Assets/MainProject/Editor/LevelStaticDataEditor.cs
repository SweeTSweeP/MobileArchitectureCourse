using System.Linq;
using MainProject.Scripts.Logic;
using MainProject.Scripts.Logic.EnemySpawners;
using MainProject.Scripts.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainProject.Editor
{
    [CustomEditor(typeof(LevelStaticDataInfo))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var levelData = (LevelStaticDataInfo)target;

            if (GUILayout.Button("Collect"))
            {
                levelData.EnemySpawners = FindObjectsOfType<SpawnMarker>()
                    .Select(s => new EnemySpawnerData(s.GetComponent<UniqueId>().Id, s.MonsterTypeId, s.transform.position))
                    .ToList();

                levelData.LevelKey = SceneManager.GetActiveScene().name;
            }
            
            EditorUtility.SetDirty(target);
        }
    }
}