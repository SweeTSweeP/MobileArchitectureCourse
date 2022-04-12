using System;
using CodeBase.Logic;
using MainProject.Scripts.CameraLogic;
using UnityEngine;
using Object = UnityEngine.Object;


namespace MainProject.Scripts.Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPoint = "InitialPoint";
        private const string HeroPath = "Characters/Hero/hero";
        private const string UiPath = "UI/Hud";

        private GameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
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
            var initialPoint = GameObject.FindWithTag(InitialPoint);
            var hero = Instantiate(HeroPath, at: initialPoint.transform.position);
            
            Instantiate(UiPath);
            
            CameraFollow(hero);
            
            _gameStateMachine.Enter<GameLoopState>();
        }

        private static GameObject Instantiate(string path)
        {
            var prefab = Resources.Load(path);
            return (GameObject) Object.Instantiate(prefab);
        }
        
        private static GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load(path);
            return (GameObject) Object.Instantiate(prefab, at, Quaternion.identity);
        }
        
        private void CameraFollow(GameObject hero) => 
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
    }
}