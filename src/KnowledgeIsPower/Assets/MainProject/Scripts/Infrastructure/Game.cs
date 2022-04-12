using CodeBase.Logic;
using MainProject.Scripts.Services;

namespace MainProject.Scripts.Infrastructure
{
    public class Game
    {
        public static IInputService InputService;
        public GameStateMachine _stateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            _stateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain);
        }
    }
}