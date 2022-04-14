using MainProject.Scripts.Infrastructure.AssetManagement;
using UnityEngine;

namespace MainProject.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public GameObject CreateHero(GameObject at) => 
            _assets.Instantiate(AssetPath.HeroPath, at: at.transform.position);

        public void CreateHud() => 
            _assets.Instantiate(AssetPath.UiPath);
    }
}