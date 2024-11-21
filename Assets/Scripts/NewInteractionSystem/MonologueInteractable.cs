using UnityEngine;

namespace NewInteractionSystem
{
    public class MonologueInteractable : Interactable
    {
        [SerializeField] private GameObject monologueGameObject;
        [SerializeField] private bool BlockInput;
        
        public bool BlockInteraction { get; set; }
        
        public override void Interact()
        {
            if (BlockInteraction) return;
            if (executeOnce && triggered)
            {
                return;
                //setactive(false)
                //Destroy(gameObject);
            }
            
            monologueGameObject.SetActive(!monologueGameObject.activeInHierarchy);
            /*if (BlockInput)
            {
                if (monologueGameObject.activeInHierarchy)
                {
                    input.DisablePlayerInput();
                }
                else
                {
                    input.EnablePlayerInput();
                }
            }*/

        }
    }
}