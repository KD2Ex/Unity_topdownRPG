using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

[Serializable]
public struct Video
{
    public VideoPlayer VideoPlayer;
    public bool AutoPlayNext;
}

public class VideoSequence : MonoBehaviour
{
    [SerializeField] private InputReader input;
    
    [SerializeField] private Video[] vods;
    [SerializeField] private RawImage rawImage;

    private VideoPlayer playing;
    private byte current = 0;

    public UnityEvent OnSequenceEnd;
    
    private void Awake()
    {
        PrepareVods();

        rawImage.gameObject.SetActive(false);

        foreach (var vod in vods)
        {
            if (!vod.AutoPlayNext) continue;
            vod.VideoPlayer.loopPointReached += source =>
            {
                PlayNext();
            };
        }
    }

    private void OnEnable()
    {
        input.CutsceneInteractEvent += PlayNext;
    }

    private void OnDisable()
    {
        input.CutsceneInteractEvent -= PlayNext;
    }

    private Coroutine coroutine;
    
    public void PlayNext()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        
        if (!rawImage.gameObject.activeInHierarchy)
        {
            rawImage.gameObject.SetActive(true);
            GameManager.instance.PauseTime();
        }
        
        if (playing) playing.Stop();
        if (current == vods.Length)
        {
            var render = rawImage.texture as RenderTexture;
            
            if (render) render.Release();
            rawImage.gameObject.SetActive(false);

            playing.time = 0f;
            
            playing = null;
            coroutine = null;
            
            PrepareVods();
            
            GameManager.instance.UnpauseTime();
            current = 0;
            
            OnSequenceEnd?.Invoke();
            input.EnableUIInput();
            input.EnablePlayerInput(true);
            return;
        }

        input.DisableUIInput();
        input.EnablePlayerInput(false);
        vods[current].VideoPlayer.Play();
        playing = vods[current].VideoPlayer;

        Debug.Log(current);
        Debug.Log(vods[current].VideoPlayer.name);
        
        /*
        if (vods[current].AutoPlayNext)
        {
            Debug.Log("vods[current] length: " + vods[current].VideoPlayer.length);

            coroutine = StartCoroutine(PlayVideo());
        }

        
        vods[current].VideoPlayer.loopPointReached += (handle) =>
        {
            Debug.Log($"loop point reached by: {handle.name}");
        };*/
        
        current++;
    }

    private IEnumerator PlayVideo()
    {
        Debug.Log(playing.length);
        yield return new WaitForSecondsRealtime(Convert.ToSingle(playing.length));
        coroutine = null;
        PlayNext();
    }

    private void PrepareVods()
    {
        foreach (var vod in vods)
        {
            vod.VideoPlayer.Prepare();
        }
    }
}