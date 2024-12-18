using UnityEngine;

[CreateAssetMenu(fileName = "MediaData", menuName = "Console/Media Data")]
public class MediaData : ScriptableObject
{
    public AudioClip audioClip;
    public AnimationClip animationClip;
}