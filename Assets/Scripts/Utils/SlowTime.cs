using UnityEngine;

public class SlowTime : MonoBehaviour
{
    [SerializeField] private float value;
    
    public void Execute()
    {
        GameManager.instance.ApplyTimeScale(value);
    }

    public void Revert()
    {
        GameManager.instance.ApplyTimeScale(1f);
    }
}
