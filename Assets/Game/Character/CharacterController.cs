using DG.Tweening;
using Game.Application.Commands;
using Game.Points;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Character
{
    public class CharacterController
    {
        private readonly IInstantiator _instantiator;
        private readonly CharacterConfig _characterConfig;
        private Character _character;
        private Tween _tween;
        private bool _isСollector;

        private Vector3 _startPoseEulerAngles;
        private Vector3 _startRotationPoseEulerAngles;
        
        private static readonly int State = Animator.StringToHash("State");
        private static readonly int Work = Animator.StringToHash("Work");

        public CharacterController(
            IInstantiator instantiator,
            CharacterConfig characterConfig)
        {
            _instantiator = instantiator;
            _characterConfig = characterConfig;

            MessageBroker.Default.Receive<OnRestartMessage>()
                .Subscribe((message => ResetCharacter()));
        }

        public void SpawnCharacter()
        {
            _character = _instantiator
                .InstantiatePrefabResource("Character")
                .GetComponent<Character>();

            _startPoseEulerAngles = _character.transform.eulerAngles;

            _character.OnNewTargetEvent += SetTarget;
        }

        private void ResetCharacter()
        {
            _character.transform.position = Vector3.zero;
            _character.transform.eulerAngles = _startPoseEulerAngles;
        }

        private void SetTarget(Point point)
        {
            if (_isСollector)
            {
                return;
            }
            
            _tween.Kill();
            
            _character.Animator.SetInteger(State, 1);
            
            var duration = Vector3.Distance(point.transform.position, _character.transform.position);

            _tween = DOTween.To(() => _character.transform.position,
                    x => _character.transform.position = x,
                    point.transform.position,
                    duration / _characterConfig.Speed)
                .SetEase(Ease.Linear)
                .OnComplete((() =>
                {
                    _isСollector = true;
                    _character.Animator.SetTrigger(Work);
                    DOVirtual.DelayedCall(0.4f, () =>
                    {
                        point.Pickup();
                        _character.Animator.SetInteger(State, 0);
                        _isСollector = false;
                    });
                }));

            var direction = point.transform.position - _character.transform.position;
            var angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;

            _character.transform.DOLocalRotate(new Vector3(0f,0f, angle + 180f), _characterConfig.RotationSpeed);
        }
    }
}
