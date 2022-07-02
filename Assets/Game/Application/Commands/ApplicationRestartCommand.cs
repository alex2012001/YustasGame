using Game.Points;
using Game.UI.Realization;
using UniRx;

namespace Game.Application.Commands
{
    public struct OnRestartMessage
    { }
    
    public class ApplicationRestartCommand : Command.Command
    {
        public override void Do()
        {
            MessageBroker.Default.Publish(new OnRestartMessage());
            MessageBroker.Default.Publish(new OnUpdateLivesMessage(OnUpdateLivesMessage.Refresh));
            
            PointsSpawner.Instance.Spawn();
            
            OnDone?.Invoke();
        }
    }
}
