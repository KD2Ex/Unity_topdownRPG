using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void LateUpdate()
    {
        if (!target) return;
        var pos = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = pos;
    }

}
