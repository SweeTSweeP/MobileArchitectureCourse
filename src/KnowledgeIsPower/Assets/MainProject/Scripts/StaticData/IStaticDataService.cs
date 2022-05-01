using CodeBase.StaticData;
using MainProject.Scripts.Infrastructure.Services;

namespace MainProject.Scripts.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadMonsters();
        MonsterStaticData ForMonster(MonsterTypeId typeId);
    }
}