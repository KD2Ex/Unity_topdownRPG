using TMPro;
using UnityEngine;

public class ConsoleTextField : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private Console console;
    [SerializeField] private PauseMenu menu;
    
    [SerializeField] private TMP_InputField InputField;

    private void OnEnable()
    {
        GameManager.instance.ConsoleOpen = true;
        input.UIEnterEvent += EnterCommand;
        InputField.Select();
        InputField.ActivateInputField();
    }

    private void OnDisable()
    {
        GameManager.instance.ConsoleOpen = false;
        input.UIEnterEvent -= EnterCommand;
    }

    public void EnterCommand()
    {
        var value = InputField.text;
        
        Debug.Log($"Console string: {value}");
        var success = console.TryExecute(value);

        if (success)
        {
            InputField.text = "";
            menu.gameObject.SetActive(false);
            // play sound, enabled visuals
        }
    }
}