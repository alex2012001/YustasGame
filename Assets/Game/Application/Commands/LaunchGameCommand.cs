using DG.Tweening;
using Game.Character;
using Game.Points;
using Game.UI.Realization;
using Zenject;

namespace Game.Application.Commands
{
    public class LaunchGameCommand : Command.Command
    {
        private readonly GameCanvas _gameCanvas;
        private readonly IInstantiator _instantiator;

        public LaunchGameCommand(
            GameCanvas gameCanvas,
            IInstantiator instantiator)
        {
            _gameCanvas = gameCanvas;
            _instantiator = instantiator;
        }
        
        public override void Do()
        {
            _gameCanvas.Show<UILivesCounter>();
            _gameCanvas.Show<UIScoreCounter>();

            var characterController = _instantiator.Instantiate<CharacterController>();
            characterController.SpawnCharacter();
            
            //Delay for start spawn points
            DOVirtual.DelayedCall(2f, () =>
            {
               var pointsSpawner = _instantiator.Instantiate<PointsSpawner>();
               pointsSpawner.Spawn();
               OnDone?.Invoke();
            });
        }
    }
}