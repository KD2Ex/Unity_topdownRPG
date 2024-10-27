using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public Player Player;
    public SaveDrops Drops;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
