using System;
using System.Collections;
using System.Transactions;
using UnityEngine;
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
    [SerializeField] private Video[] vods;
    [SerializeField] private RawImage rawImage;

    private VideoPlayer playing;
    private byte current = 0;
    
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
            Time.timeScale = 0f;
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
            
            Time.timeScale = 1f;
            current = 0;
            
            return;
        }


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