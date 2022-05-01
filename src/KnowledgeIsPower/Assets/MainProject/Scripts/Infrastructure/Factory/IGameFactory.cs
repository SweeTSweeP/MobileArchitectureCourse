using System.Collections.Generic;
using CodeBase.StaticData;
using MainProject.Scripts.Infrastructure.Services;
using MainProject.Scripts.Infrastructure.Services.PersistentProgress;
using MainProject.Scripts.Logic;
using UnityEngine;

namespace MainProject.Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        GameObject CreateHero(GameObject at);
        GameObject CreateHud();
        void Cleanup();
        void Register(ISavedProgressReader progressReader);
        GameObject CreateMonster(MonsterTypeId typeId, Transform parent);
    }
}