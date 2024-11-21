using System;
using UnityEngine;

namespace NewInteractionSystem
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] protected InputReader input;
        [SerializeField] protected bool executeOnce;

        protected bool triggered = false;
        protected Player player => GameManager.instance.Player;
        
        protected virtual void OnEnable()
        {
            input.CutsceneInteractEvent += Interact;
            input.CutsceneInteractEvent += Trigger;
        }

        protected virtual void OnDisable()
        {
            input.CutsceneInteractEvent -= Interact;
            input.CutsceneInteractEvent -= Trigger;
        }

        public virtual void Interact()
        {
            
        }

        private void Trigger() => triggered = true;
    }
}