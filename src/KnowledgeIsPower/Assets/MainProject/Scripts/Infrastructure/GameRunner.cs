using UnityEngine;

namespace MainProject.Scripts.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper _gameBootstrapperPrefab;
        
        private void Awake()
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();

            if (!bootstrapper)
                Instantiate(_gameBootstrapperPrefab);
        }
    }
}