using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public Player Player;
    public SaveDrops Drops;

    [SerializeField] private FloatVariable timeScale;
    public float TimeScale => timeScale.Value;
    public bool ConsoleOpen;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        ApplyTimeScale(TimeScale);
    }

    public void ApplyTimeScale(float value)
    {
        timeScale.Value = value;
        Time.timeScale = value;
    }

    public void PauseTime()
    {
        Time.timeScale = 0f;
    }

    public void UnpauseTime()
    {
        Time.timeScale = TimeScale;
    }

    public float GetTimeScale()
    {
        return timeScale.Value;   
    }
}
