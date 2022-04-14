using CodeBase.Logic;
using MainProject.Scripts.CameraLogic;
using MainProject.Scripts.Infrastructure.Factory;
using UnityEngine;


namespace MainProject.Scripts.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPoint = "InitialPoint";

        private GameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(
            GameStateMachine gameStateMachine, 
            SceneLoader sceneLoader, 
            LoadingCurtain loadingCurtain, 
            IGameFactory gameFactory)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, onLoaded);
        }

        public void Exit() => 
            _loadingCurtain.Hide();

        private void onLoaded()
        {
            var hero = _gameFactory.CreateHero(at: GameObject.FindWithTag(InitialPoint));

            _gameFactory.CreateHud();
            
            CameraFollow(hero);
            
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void CameraFollow(GameObject hero) => 
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
    }
}