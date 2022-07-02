using Game.Application.Commands;
using Game.UI.Realization;
using UniRx;
using UnityEngine;

namespace Game.Points
{
    public class Point : MonoBehaviour
    {
        public PointEffects Effect = PointEffects.Standard;

        private void Awake()
        {
            MessageBroker.Default.Receive<OnRestartMessage>()
                .Subscribe((message => Destroy(gameObject)))
                .AddTo(this);
        }

        public void SetEffect(PointEffects effect)
        {
            Effect = effect;
        }

        public void Pickup()
        {
            switch (Effect)
            {
                case PointEffects.Standard:
                    MessageBroker.Default.Publish(new OnUpdateCounterMessage());
                    break;
                case PointEffects.Rotten:
                    MessageBroker.Default.Publish(new OnUpdateLivesMessage(OnUpdateLivesMessage.Decrease));
                    break;
                case PointEffects.Magic:
                    MessageBroker.Default.Publish(new OnUpdateLivesMessage(OnUpdateLivesMessage.Refresh));
                    break;
            }
            
            Debug.Log(Effect);
            
            MessageBroker.Default.Publish(new OnTakePointMessage());
            
            Destroy(gameObject);
        }
    }
}
