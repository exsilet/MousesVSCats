using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Service;
using CodeBase.Infrastructure.Service.StaticData;

namespace CodeBase.Infrastructure.State
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial, onLoader: EnterLoadLevel);
        }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadMenuState, string>("Game");

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            RegisterStaticData();
            
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            //_services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());

            // _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IAssetProvider>()));
            // _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));

            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssetProvider>(),
                _services.Single<IStaticDataService>()));
        }
        
        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.Load();
            _services.RegisterSingle(staticData);
        }

        // private void EnterLoadLevel()
        // {
        //     //_stateMachine.Enter<LoadProgressState>();
        // }
    }
}