using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Inputs", menuName = "SO/Player/Input")]
public class InputReader : ScriptableObject, PlayerControls.IPlayerActions, PlayerControls.IUIActions
{
    private static InputReader instance;

    private PlayerControls input;
    
    public  UnityAction<Vector2> MoveEvent;
    public  UnityAction<bool> AttackEvent;
    
    private static bool WasMoovedThisFrame;

    private void OnEnable()
    {
        if (input == null)
        {
            input = new PlayerControls();
            
            input.Player.SetCallbacks(this);
            input.UI.SetCallbacks(this);
        }
        
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started) return;
        
        var value = context.ReadValue<Vector2>();
        MoveEvent.Invoke(value);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed) return;
        
        AttackEvent.Invoke(context.ReadValue<float>() > .01f);
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnDev(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
