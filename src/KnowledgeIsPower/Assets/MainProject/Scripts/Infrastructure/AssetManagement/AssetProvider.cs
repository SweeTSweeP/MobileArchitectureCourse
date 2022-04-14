using UnityEngine;

namespace MainProject.Scripts.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load(path);
            return (GameObject) Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load(path);
            return (GameObject) Object.Instantiate(prefab, at, Quaternion.identity);
        }
    }
}