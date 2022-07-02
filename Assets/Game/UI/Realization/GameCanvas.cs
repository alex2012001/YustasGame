using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.UI.Realization
{
    public class GameCanvas : MonoBehaviour
    {
        [SerializeField] private Transform poolContainer;
        [SerializeField] private Transform viewContainer;
        
        private Dictionary<Type, GameObject> _instViews = new Dictionary<Type, GameObject>();
        private IInstantiator _instantiator;
        
        [Inject]
        private void Inject(
            IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public void Init()
        {
            var windows = Resources.LoadAll("UIWindows", typeof(UIWindow));
            for (int i = 0; i < windows.Length; i++)
            {
                var window = _instantiator.InstantiatePrefab(windows[i], poolContainer);
                _instViews.Add(windows[i].GetType(),window);
            }
        }
        
        //TODO: Add hide method
        public T Show<T>() where T : UIWindow
        {
            var type = typeof(T);
            
            if (!_instViews.ContainsKey(type))
            {
                return null;
            }

            var view = _instViews[type];

            view.transform.SetParent(viewContainer, false);

            var component = view.GetComponent<T>();

            component.Show();
            
            return component;
        }
    }
}
