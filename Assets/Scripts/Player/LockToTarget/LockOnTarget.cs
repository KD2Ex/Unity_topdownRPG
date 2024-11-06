using UnityEngine;

public class LockOnTarget : MonoBehaviour
{
    [SerializeField] private GameObject crosshair;
    [SerializeField] private EnemyRuntimeSet set;
    
    private Enemy[] enemies => set.Items.ToArray();

    public Vector3 CrosshairPos => crosshair.transform.position;
    public bool Locked { get; private set; }
    
    public void LockToNearest()
    {
        if (enemies.Length == 0)
        {
            Locked = false;
            return;
        }
        
        Transform nearest = enemies[0].transform;
        
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float minDist = (enemies[0].transform.position - mousePos).magnitude;
        
        foreach (var enemy in enemies)
        {
            var dist = (enemy.transform.position - mousePos).magnitude;
            if (dist < minDist)
            {
                nearest = enemy.transform;
                minDist = dist;
            }
        }

        Locked = true;
        crosshair.transform.SetParent(nearest);
        crosshair.transform.localPosition = Vector3.zero;
        crosshair.SetActive(true);
    }

    private void Update()
    {
        if (!crosshair.activeInHierarchy)
        {
            Locked = false;
        }
    }
}