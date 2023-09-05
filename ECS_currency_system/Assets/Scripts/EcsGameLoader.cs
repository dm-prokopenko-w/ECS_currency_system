using System.Collections.Generic;
using Currency;
using UnityEngine;
using Leopotam.Ecs;
using Save;
using Voody.UniLeo;

namespace Core
{
    public class EcsGameLoader : MonoBehaviour
    {
        [SerializeField] private AllConfigs _allConfigs;
        private EcsWorld _world;
        private EcsSystems _systems;

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            _systems.ConvertScene();

            AddInjections();
            AddOneFrame();
            AddSystem();

            _systems.Init();
        }

        private void AddInjections()
        {
            _systems.Inject(_allConfigs);
        }

        private void AddOneFrame() 
        {
            
        }

        private void AddSystem()
        {
            _systems
                .Add(new CurrencySystem());
        }

        private void Update()
        {
            _systems.Run();
        }

        private void OnDestroy()
        {
            _systems.Destroy();
            _systems = null;
            _world.Destroy();
            _world = null;
        }
    }
}