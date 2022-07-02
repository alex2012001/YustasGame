using System;
using UnityEngine;

namespace Game.UI
{
    public abstract class UIWindow : MonoBehaviour
    {
        public abstract void Show();
        
        public abstract void Hide(Action onHide = null);
    }
}