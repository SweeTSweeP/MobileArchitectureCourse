using CodeBase.Logic;
using MainProject.Scripts.Infrastructure.States;
using UnityEngine;

namespace MainProject.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _curtain;
        
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, _curtain);
            _game._stateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}
