using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CommandsMediaPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private Animator animator;
    [SerializeField] private Image image;
    private void Awake()
    {
        GameManager.instance.CommandsMediaPlayer = this;
    }

    public void PlaySound(AudioClip clip)
    {
        if (source.isPlaying) source.Stop();
        
        source.clip = clip;
        source.Play();
    }
    
    public void PlayAnimation(AnimationClip clip)
    {
        image.gameObject.SetActive(true);
        animator.Play(clip.name, 0, 0);
        StopAllCoroutines();
        StartCoroutine(Coroutines.WaitForRealTimeSeconds(clip.length, () => image.gameObject.SetActive(false)));
    }
    
    public void Execute(MediaData data)
    {
        PlaySound(data.audioClip);
        PlayAnimation(data.animationClip);
    }
    
}