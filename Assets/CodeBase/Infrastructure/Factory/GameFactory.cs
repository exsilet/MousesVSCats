using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Service.StaticData;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        // public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        // public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private GameObject _heroGameObject;
        
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;

        public GameFactory(IAssetProvider assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
        }

        public GameObject CreateHero(GameObject at)
        {
            var hero = _assets.Instantiate(path: AssetPath.HeroPath, at: at.transform.position);

            // RegisterProgressWatchers(hero);
            //
            // foreach (OpenWindowButton windowButton in hero.GetComponentsInChildren<OpenWindowButton>())
            //     windowButton.Construct(_windowService);

            return hero;
        }

        public GameObject CreateHud()
        {
            GameObject hud = _assets.Instantiate(AssetPath.HudPath);
            //RegisterProgressWatchers(hud);
            return hud;
        }

        public GameObject CreateHudMenu()
        {
            GameObject hudMenu = _assets.Instantiate(AssetPath.HudMenuPath);
            //RegisterProgressWatchers(hudMenu);
            return hudMenu;
        }

        public GameObject CreatTower(TowerTypeID typeId, Transform parent)
        {
            TowerStaticData towerData = _staticData.ForTower(typeId);
            GameObject tower = Object.Instantiate(towerData.Prefab, parent.position, Quaternion.identity, parent);

            // var health = tower.GetComponent<MinionHealth>();
            // health.Current = towerData.CurrentHP;
            // health.Max = towerData.MaxHP;
        
            return tower;
        }

        // public void Cleanup()
        // {            
        //     ProgressReaders.Clear();
        //     ProgressWriters.Clear();
        //     
        //     PlayerPrefs.DeleteAll();
        //     PlayerPrefs.Save();
        // }

        // private void RegisterProgressWatchers(GameObject hero)
        // {
        //     foreach (ISavedProgressReader progressReader in hero.GetComponentsInChildren<ISavedProgressReader>())
        //     {
        //         Register(progressReader);
        //     }
        // }
        //
        // private void Register(ISavedProgressReader progressReader)
        // {
        //     if (progressReader is ISavedProgress progressWriter)
        //         ProgressWriters.Add(progressWriter);
        //
        //     ProgressReaders.Add(progressReader);
        // }
    }
}