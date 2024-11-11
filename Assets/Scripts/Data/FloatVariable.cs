using UnityEngine;

[CreateAssetMenu(fileName = "Float", menuName = "SO/Data/Float")]
public class FloatVariable : ScriptableObject
{
    public float Value;
}


public class SavePlayerData
{
    public Vector3 Position;
    public int Coins;

    public SavePlayerData(Vector3 position, int coins)
    {
        Position = position;
        Coins = coins;
    }
}