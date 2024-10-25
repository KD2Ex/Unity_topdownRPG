using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [HideInInspector] public Player Player;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
