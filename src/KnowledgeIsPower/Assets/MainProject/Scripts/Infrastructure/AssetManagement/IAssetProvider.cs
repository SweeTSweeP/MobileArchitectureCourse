using MainProject.Scripts.Infrastructure.Services;
using UnityEngine;

namespace MainProject.Scripts.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
    }
}