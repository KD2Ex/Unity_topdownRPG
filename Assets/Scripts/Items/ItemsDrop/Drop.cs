using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] private GameObject item;
    
    public void Spawn()
    {
        Instantiate(item, transform.position, Quaternion.identity);
    }
}
