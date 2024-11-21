using UnityEngine;
using UnityEngine.Events;

namespace NewInteractionSystem
{
    public class OnEnableEvent : MonoBehaviour
    {
        public UnityEvent Event;
        
        private void OnEnable()
        {
            Event?.Invoke();
        }
    }
}