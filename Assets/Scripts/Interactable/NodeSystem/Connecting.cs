using UnityEngine;
using UnityEngine.Events;

public class Connecting : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private GameObject next;
    
    private float elapsed = 0f;

    public UnityEvent OnClipFinished;
    
    private void OnEnable()
    {
        source.Play();
    }

    private void OnDisable()
    {
        OnClipFinished?.Invoke();
    }

    private void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed < source.clip.length) return;
        
        gameObject.SetActive(false);
    }
}