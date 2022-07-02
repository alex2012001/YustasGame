using System;
using Game.Application.Commands;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.UI.Realization
{
    public struct OnUpdateLivesMessage
    {
        public const string Refresh = nameof(Refresh);
        public const string Decrease = nameof(Decrease);

        public string Name { get; }
        
        public OnUpdateLivesMessage(string name)
        {
            Name = name;
        }
    }
    
    public class UILivesCounter : UIWindow
    {
        [SerializeField] private TextMeshProUGUI livesText;

        private int _livesValue;

        private const int LivesLimit = 5;
        private const string LivesText = "Lives : ";

        private IInstantiator _instantiator;
        
        [Inject]
        private void Inject(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }
        
        private void Awake()
        {
            MessageBroker.Default.Receive<OnUpdateLivesMessage>()
                .Where(x => x.Name == OnUpdateLivesMessage.Refresh)
                .Subscribe(_ => RefreshLives())
                .AddTo(this);

            MessageBroker.Default.Receive<OnUpdateLivesMessage>()
                .Where(x => x.Name == OnUpdateLivesMessage.Decrease)
                .Subscribe(_ => DecreaseLives())
                .AddTo(this);
        }

        public override void Show()
        {
            _livesValue = LivesLimit;
            UpdateText();
        }

        public override void Hide(Action onHide = null)
        {
            
        }

        private void UpdateText()
        {
            livesText.SetText(LivesText + _livesValue + "/" + LivesLimit);
        }
        
        private void RefreshLives()
        {
            _livesValue = LivesLimit;
            UpdateText();
        }
        
        private void DecreaseLives()
        {
            _livesValue--;

            UpdateText();
            
            if (_livesValue == 0)
            {
                var command = _instantiator.Instantiate<ApplicationRestartCommand>();
                command.Do();
            }
        }
    }
}