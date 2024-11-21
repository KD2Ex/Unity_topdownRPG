using System;
using UnityEngine;
using UnityEngine.Events;

namespace NewInteractionSystem
{
    public class OnDisableEvent : MonoBehaviour
    {
        public UnityEvent Event;

        public void OnDisable()
        {
            Event?.Invoke();
        }
    }
}