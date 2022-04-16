using System;
using System.Collections.Generic;
using MainProject.Scripts.Infrastructure.Services;
using MainProject.Scripts.Infrastructure.Services.PersistentProgress;
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
        void CreateHud();
        void Cleanup();
    }
}