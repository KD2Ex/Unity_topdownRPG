using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Inputs", menuName = "SO/Player/Input")]
public class InputReader : ScriptableObject, 
    PlayerControls.IPlayerActions, 
    PlayerControls.IUIActions
{
    private static InputReader instance;

    private PlayerControls input;
    private PlayerInput playerInput;
    
    public  UnityAction<Vector2> MoveEvent;
    public  UnityAction<bool> AttackEvent;
    public UnityAction DevEvent;
    public UnityAction InteractEvent;
    public UnityAction DashEvent;
    public UnityAction ParryEvent;
    public UnityAction LockEvent;
    public UnityAction PauseEvent;

    public Action UIEnterEvent;
    
    private static bool WasMoovedThisFrame;

    public void EnablePlayerInput(bool value)
    {
        if (value) input.Player.Enable();
        else input.Player.Disable();
    }

    public void EnableUIInput()
    {
        input.UI.Enable();
    }

    public void DisableUIInput()
    {
        input.UI.Disable();
    }
    
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
        MoveEvent?.Invoke(value);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed) return;
        
        AttackEvent?.Invoke(context.ReadValue<float>() > .01f);
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        DashEvent?.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        InteractEvent?.Invoke();
    }

    public void OnDev(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        
        DevEvent?.Invoke();
    }

    public void OnParry(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        ParryEvent?.Invoke();
    }

    public void OnLock(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        LockEvent?.Invoke();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        PauseEvent?.Invoke();
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        UIEnterEvent?.Invoke();
    }
}
