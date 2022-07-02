using Game.UI.Realization;

namespace Game.Application.Commands
{
    public class InitGameSceneCommand : Command.Command
    {
        private readonly GameCanvas _gameCanvas;

        public InitGameSceneCommand(
            GameCanvas gameCanvas)
        {
            _gameCanvas = gameCanvas;
        }
        
        public override void Do()
        {
            _gameCanvas.Init();
            
            OnDone?.Invoke();
        }
    }
}