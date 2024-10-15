using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float lifeTime;

    private float elapsedTime;

    private void OnEnable()
    {
        elapsedTime = 0f;
    }


    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        
        if (elapsedTime > lifeTime) gameObject.SetActive(false);
    }
}
