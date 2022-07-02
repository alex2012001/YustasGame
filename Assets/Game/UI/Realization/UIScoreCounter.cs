using System;
using Game.Application.Commands;
using TMPro;
using UniRx;
using UnityEngine;

namespace Game.UI.Realization
{
    public struct OnUpdateCounterMessage
    { }
    
    public class UIScoreCounter : UIWindow
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        private int _counterValue;
        
        private const string ScoreText = "Scores: ";

        private void Awake()
        {
            MessageBroker.Default.Receive<OnRestartMessage>()
                .Subscribe(_ => Refresh())
                .AddTo(this);
            
            MessageBroker.Default.Receive<OnUpdateCounterMessage>()
                .Subscribe(_ => AddToCounter())
                .AddTo(this);
        }

        public override void Show()
        {
            _counterValue = 0;
            UpdateText();
        }

        public override void Hide(Action onHide = null)
        {
            
        }

        private void UpdateText()
        {
            scoreText.SetText(ScoreText + _counterValue);
        }

        private void AddToCounter()
        {
            _counterValue++;
            UpdateText();
        }
        
        private void Refresh()
        {
            _counterValue = 0;
            UpdateText();
        }
    }
}