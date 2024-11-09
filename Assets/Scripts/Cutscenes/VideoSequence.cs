using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoSequence : MonoBehaviour
{
    [SerializeField] private VideoPlayer[] vods;
    [SerializeField] private RawImage rawImage;
    
    private VideoPlayer playing;
    private byte current = 0;
    
    private void Awake()
    {
        foreach (var vod in vods)
        {
            vod.Prepare();
        }

        rawImage.gameObject.SetActive(false);
    }

    public void PlayNext()
    {
        if (!rawImage.gameObject.activeInHierarchy)
        {
            rawImage.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        
        if (playing) playing.Stop();
        if (current == vods.Length)
        {
            rawImage.gameObject.SetActive(false);
            var render = rawImage.texture as RenderTexture;
            if (render) render.Release();
            Time.timeScale = 1f;
            current = 0;
            return;
        }
        
        vods[current].Play();
        playing = vods[current];
        
        current++;
    } 
}