using CodeBase.Infrastructure.StaticData;

namespace CodeBase.Infrastructure.Service.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        TowerStaticData ForTower(TowerTypeID typeID);
    }
}