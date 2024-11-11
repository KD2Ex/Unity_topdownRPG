using UnityEngine;

public class CanvasAnimation : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private Animator animator;
    
    private void OnEnable()
    {
        input.DisablePlayerInput();
        input.DisableUIInput();
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        input.EnablePlayerInput();
        input.EnableUIInput();
        Time.timeScale = 1f;
    }

    private void Update()
    {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime > 1f)
        {
            gameObject.SetActive(false);

            Vector2 vector;
            
            
        }
    }
}
