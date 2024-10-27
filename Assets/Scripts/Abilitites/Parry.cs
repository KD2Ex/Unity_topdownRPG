using UnityEngine;

public class Parry : MonoBehaviour
{
    [SerializeField] private GameObject vfx;
    [SerializeField] private GameObject shield;
    [SerializeField] private float time;

    public bool Running { get; private set; }
    
    public void Execute()
    {
        if (Running) return;

        StartCoroutine(Coroutines.WaitFor(time, ParryStart, ParryEnd));
    }

    private void ParryStart()
    {
        Running = true;
        shield.SetActive(true);

    }

    private void ParryEnd()
    {
        Running = false;
        shield.SetActive(false);
    }
}
