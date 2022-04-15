using UnityEditor;
using UnityEngine;

namespace MainProject.Editor
{
    public class Tools
    {
        [MenuItem("Ruslan/Tools/ClearPrefs")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}
