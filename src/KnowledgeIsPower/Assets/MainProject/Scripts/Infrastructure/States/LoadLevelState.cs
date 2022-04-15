using CodeBase.Logic;
using MainProject.Scripts.CameraLogic;
using MainProject.Scripts.Infrastructure.Factory;
using MainProject.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.Windows;


namespace MainProject.Scripts.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPoint = "InitialPoint";

        private GameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public LoadLevelState(
            GameStateMachine gameStateMachine, 
            SceneLoader sceneLoader, 
            LoadingCurtain loadingCurtain, 
            IGameFactory gameFactory,
            IPersistentProgressService progressService)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, onLoaded);
        }

        public void Exit() => 
            _loadingCurtain.Hide();

        private void onLoaded()
        {
            InitGameWorld();
            InformProgressReaders();
            
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (var progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }
        }

        private void InitGameWorld()
        {
            var hero = _gameFactory.CreateHero(at: GameObject.FindWithTag(InitialPoint));

            _gameFactory.CreateHud();

            CameraFollow(hero);
        }

        private void CameraFollow(GameObject hero) => 
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
    }
}