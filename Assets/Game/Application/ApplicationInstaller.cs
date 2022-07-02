using Game.Character;
using Game.Points;
using Game.UI.Realization;
using UnityEngine;
using Zenject;

namespace Game.Application
{
    public class ApplicationInstaller : MonoInstaller
    {
        [SerializeField] private PointsConfig pointsConfig;
        [SerializeField] private CharacterConfig characterConfig;
        
        public override void InstallBindings()
        {
            Container
                .Bind<GameCanvas>()
                .FromComponentInNewPrefabResource("GameCanvas")
                .AsSingle();

            Container
                .Bind<PointsConfig>()
                .FromInstance(pointsConfig)
                .AsSingle();

            Container
                .Bind<PointsEffector>()
                .AsSingle();
            
            Container
                .Bind<CharacterConfig>()
                .FromInstance(characterConfig)
                .AsSingle();
            
            Container
                .Bind<ApplicationLauncher>()
                .AsSingle()
                .NonLazy();
        }
    }
}
