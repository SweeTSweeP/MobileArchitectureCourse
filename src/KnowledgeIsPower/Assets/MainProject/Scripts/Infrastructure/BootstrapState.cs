using System;
using MainProject.Scripts.Services;
using UnityEngine;

namespace MainProject.Scripts.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "InitialScene";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(InitialScene, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel() => 
            _gameStateMachine.Enter<LoadLevelState, string>("MainLevel");

        private void RegisterServices() => 
            Game.InputService = RegisterInputService();

        public void Exit()
        {
        }

        private static IInputService RegisterInputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            
            return new MobileInputService();
        }
    }
}