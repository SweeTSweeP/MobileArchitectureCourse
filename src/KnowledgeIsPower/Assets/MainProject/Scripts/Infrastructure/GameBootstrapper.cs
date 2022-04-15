using CodeBase.Logic;
using MainProject.Scripts.Infrastructure.States;
using UnityEngine;

namespace MainProject.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _curtainPrefab;
        
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Instantiate(_curtainPrefab));
            _game._stateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}
