using System;
using System.Collections.Generic;
using MainProject.Scripts.Infrastructure.Services;
using MainProject.Scripts.Infrastructure.Services.PersistentProgress;
using MainProject.Scripts.Logic;
using UnityEngine;

namespace MainProject.Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        event Action HeroCreated;
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        GameObject HeroGameObject { get; set; }
        GameObject CreateHero(GameObject at);
        GameObject CreateHud();
        void Cleanup();
        void Register(ISavedProgressReader progressReader);
    }
}