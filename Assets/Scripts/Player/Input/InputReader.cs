using System;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Inputs", menuName = "SO/Player/Input")]
public class InputReader : ScriptableObject, 
    PlayerControls.IPlayerActions, 
    PlayerControls.IUIActions,
    PlayerControls.ICutsceneActions
{
    private static InputReader instance;

    private PlayerControls input;
    private PlayerInput playerInput;
    
    public Action<Vector2> MoveEvent;
    public  UnityAction<bool> AttackEvent;
    public UnityAction DevEvent;
    public UnityAction InteractEvent;
    public UnityAction DashEvent;
    public UnityAction ParryEvent;
    public UnityAction LockEvent;
    public UnityAction PauseEvent;

    public Action UIEnterEvent;
    public Action CutsceneInteractEvent;
    
    private static bool WasMoovedThisFrame;

    public void EnablePlayerInput()
    {
        input.Player.Enable();

    }

    public void DisablePlayerInput()
    {
        input.Player.Disable();
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
            input.Cutscene.SetCallbacks(this);
        }
        
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    /*public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started) return;
        
        var value = context.ReadValue<Vector2>();
        MoveEvent?.Invoke(value);
    }*/

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log(context.started);
        Debug.Log(context.performed);
        Debug.Log(context.canceled);
        
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

    public void OnCutsceneInteract(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        CutsceneInteractEvent?.Invoke();
    }
}
