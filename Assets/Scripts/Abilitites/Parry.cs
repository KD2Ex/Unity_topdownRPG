using UnityEngine;

public class Parry : MonoBehaviour
{
    [SerializeField] private GameObject vfx;
    [SerializeField] private float time;

    private bool running;
    
    public void Execute()
    {
        if (running) return;

        StartCoroutine(Coroutines.WaitFor(time, ParryStart, ParryEnd));
    }

    private void ParryStart()
    {
        running = true;
        vfx.SetActive(true);

    }

    private void ParryEnd()
    {
        running = false;
        vfx.SetActive(false);
    }
}
