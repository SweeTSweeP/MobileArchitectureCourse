using CodeBase.Logic;
using MainProject.Scripts.Infrastructure.Services;
using MainProject.Scripts.Infrastructure.States;

namespace MainProject.Scripts.Infrastructure
{
    public class Game
    {
        public GameStateMachine _stateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            _stateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container);
        }
    }
}