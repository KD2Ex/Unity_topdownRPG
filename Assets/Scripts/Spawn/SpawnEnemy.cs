using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private float seconds;
    private float radius => Camera.main.orthographicSize * 16 / 9;

    private Transform Player => GameManager.instance.Player.transform;
    
    void Start()
    {
        Debug.Log($"Radius: {radius}");
    }

    public void Execute()
    {
        StartCoroutine(Coroutines.WaitForRealTimeSeconds(seconds, Spawn));
    }

    private void Spawn()
    {
        var side = Random.Range(0, 2) == 0 ? radius * -1 : radius;
        var sidePos = new Vector3(side, 0f, 0f);
        
        var pos = Player.position + sidePos * 2f;
        var inst = Instantiate(enemy, pos, Quaternion.identity);
    }
}
