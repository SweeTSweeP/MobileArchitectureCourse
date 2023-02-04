using System.ComponentModel;
using CodeBase.Infrastructure.States;
using MainProject.Scripts.Infrastructure.AssetManagement;
using MainProject.Scripts.Infrastructure.Factory;
using MainProject.Scripts.Infrastructure.Services;
using MainProject.Scripts.Infrastructure.Services.Inputs;
using MainProject.Scripts.Infrastructure.Services.PersistentProgress;
using MainProject.Scripts.Infrastructure.Services.Randomizer;
using MainProject.Scripts.Infrastructure.Services.SaveLoad;
using MainProject.Scripts.StaticData;
using UnityEngine;

namespace MainProject.Scripts.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "InitialScene";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(InitialScene, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel() => 
            _gameStateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            RegisterStaticData();

            _services.RegisterSingle<IInputService>(InputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IRandomService>(new RandomService());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssetProvider>(),
                _services.Single<IStaticDataService>(),
                _services.Single<IRandomService>(),
                _services.Single<IPersistentProgressService>()));
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(
                _services.Single<IPersistentProgressService>(),
                _services.Single<IGameFactory>()));
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.LoadMonsters();
            _services.RegisterSingle(staticData);
        }

        private static IInputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            
            return new MobileInputService();
        }
    }
}