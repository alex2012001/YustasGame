using DG.Tweening;
using Game.Application.Commands;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Points
{
    public struct OnTakePointMessage
    { }

    public class PointsSpawner
    {
        public static PointsSpawner Instance;
        
        private int _pointsCounter;
        private Tween _tween;
        
        private readonly PointsConfig _pointsConfig;
        private readonly IInstantiator _instantiator;
        private readonly PointsEffector _pointsEffector;

        public PointsSpawner(
            PointsConfig pointsConfig,
            IInstantiator instantiator,
            PointsEffector pointsEffector)
        {
            Instance = this;
            
            _pointsConfig = pointsConfig;
            _instantiator = instantiator;
            _pointsEffector = pointsEffector;

            MessageBroker.Default.Receive<OnTakePointMessage>()
                .Subscribe((message => TakePoint()));
            
            MessageBroker.Default.Receive<OnRestartMessage>()
                .Subscribe((message => Restart()));
        }

        private void Restart()
        {
            _tween.Kill();
            _tween = null;
            _pointsCounter = 0;
        }

        private void TakePoint()
        {
            _pointsCounter--;
        }
        
        public void Spawn()
        {
            _tween = DOVirtual.DelayedCall(3f, () =>
            {
                if (_pointsCounter >= _pointsConfig.MaxNumberOfPoints)
                {
                    Spawn();
                    return;
                }
                
                var y = Random.Range(_pointsConfig.MinHeight, _pointsConfig.MaxHeight);
                var x = Random.Range(_pointsConfig.MinWeight, _pointsConfig.MaxWeight);
            
                var pos = new Vector2(x,y);
            
                var point = _instantiator.InstantiatePrefab(_pointsConfig.Prefab);
                point.transform.position = pos;

                var component = point.GetComponent<Point>();
                component.SetEffect(_pointsEffector.CreateEffect());
                
                _pointsCounter++;
                
                Spawn();
            });
        }
    }
}
