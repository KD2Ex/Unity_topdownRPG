using Unity.VisualScripting;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float multiplier;
    [SerializeField] private Vector3Data playerVelocity;

    private Vector3 Value => playerVelocity.Value;
    
    private void LateUpdate()
    {
        var value = new Vector3(0f, Value.y, 0f);
        transform.Translate(value * multiplier);    
    }
}
