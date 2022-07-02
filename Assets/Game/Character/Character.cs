using System;
using DG.Tweening;
using Game.Points;
using UnityEngine;

namespace Game.Character
{
    public class Character : MonoBehaviour
    {
        public Action<Point> OnNewTargetEvent;
        public Animator Animator => animator;

        [SerializeField] private Animator animator;

        private Camera _camera => Camera.main;
        private bool _isNeedDelay;
        
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
        
                if (Physics.Raycast(ray, out hit))
                {
                    if (!_isNeedDelay)
                    {
                        _isNeedDelay = true;
                        var point = hit.collider.GetComponent<Point>();
                        OnNewTargetEvent?.Invoke(point);
                        DOVirtual.DelayedCall(0.15f, () =>
                        {
                            _isNeedDelay = false;
                        });
                    }
                }
            }
        }
    }
}